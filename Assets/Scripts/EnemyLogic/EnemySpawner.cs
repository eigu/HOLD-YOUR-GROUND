using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _player;

    [SerializeField] private float _spawnRadius;
    [SerializeField] private Transform _fallbackPosition;

    [SerializeField] private int _numberPerWave;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private int _numberPerSpawn;

    [SerializeField] private GameEventSO _onWaveTier1End;
    [SerializeField] private GameEventSO _onWaveTier2End;
    [SerializeField] private GameEventSO _onWaveTier3End;

    private float m_currentSpawnTime;
    private int m_currentEnemySpawn;
    private bool m_isWaveInProgress;

    private float m_currentEnemySpeed;

    private bool m_triggeredAtWaveFive = false;
    private bool m_triggeredAtWaveTen = false;

    public int CurrentEnemiesRemaining { get; private set; }
    [field: SerializeField] public int CurrentWave { get; private set; }
    [field: SerializeField] public float WaveDelayTimer { get; private set; }

    private void Start()
    {
        m_currentEnemySpeed = _enemyPrefab.GetComponent<Enemy>().CurrentSpeed;

        StartWave();
    }

    private void Update()
    {
        if (!m_isWaveInProgress) return;

        m_currentSpawnTime -= Time.deltaTime;
        
        if (CurrentEnemiesRemaining <= 0)
        {
            StartCoroutine(WaveDelayCoroutine(WaveDelayTimer));
        }

        if (m_currentSpawnTime <= 0
            && m_currentEnemySpawn > 0)
        {
            SpawnEnemies();

            m_currentSpawnTime = _spawnInterval;
        }

        if (CurrentWave % 2 == 0)
        {
            if (!m_triggeredAtWaveFive)
            {
                _onWaveTier2End?.TriggerEvent();
                m_triggeredAtWaveFive = true;
            }
        }
        else
        {
            m_triggeredAtWaveFive = false;
        }

        if (CurrentWave % 3 == 0)
        {
            if (!m_triggeredAtWaveTen)
            {
                _onWaveTier3End?.TriggerEvent();
                m_triggeredAtWaveTen = true;
            }
        }
        else
        {
            m_triggeredAtWaveTen = false;
        }

    }

    public void StartWave()
    {
        CurrentWave++;
        CurrentEnemiesRemaining = _numberPerWave;
        m_currentEnemySpawn = _numberPerWave;
        m_currentSpawnTime = _spawnInterval;
        m_isWaveInProgress = true;
    }

    private void OnWaveEnd()
    {
        _onWaveTier1End?.TriggerEvent();
    }

    private IEnumerator WaveDelayCoroutine(float duration)
    {
        m_isWaveInProgress = false;

        yield return new WaitForSeconds(duration);

        OnWaveEnd();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < _numberPerSpawn; i++)
        {
            if (m_currentEnemySpawn <= 0) break;
            
            Vector3 randomSpawnPos = FindValidSpawnPosition();

            if (randomSpawnPos != Vector3.zero)
            {
                GameObject enemyInstance = Instantiate(_enemyPrefab, randomSpawnPos, Quaternion.identity);

                Vector3 directionToPlayer = (_player.position - randomSpawnPos).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

                enemyInstance.transform.rotation = targetRotation;

                Entity enemyEntity = enemyInstance.GetComponent<Entity>();

                if (enemyEntity != null)
                {
                    enemyEntity.CurrentSpeed = m_currentEnemySpeed;
                }
            }

            m_currentEnemySpawn--;
        }
    }

    private Vector3 FindValidSpawnPosition()
    {
        Vector3 bestSpawnPos = _fallbackPosition.position;
        float bestDistance = Vector3.Distance(bestSpawnPos, _player.position);

        int attempts = 0;
        const int maxAttempts = 10;

        while (attempts < maxAttempts)
        {
            Vector3 randomDirection = Random.onUnitSphere * _spawnRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, _spawnRadius, NavMesh.AllAreas))
            {
                float distanceToPlayer = Vector3.Distance(hit.position, _player.position);
                if (distanceToPlayer > bestDistance)
                {
                    bestSpawnPos = hit.position;
                    bestDistance = distanceToPlayer;
                }
            }

            attempts++;
        }

        return bestSpawnPos;
    }


    public void IncreaseNumberPerWave(int value) => _numberPerWave += value;

    public void IncreaseNumberPerSpawn(int value) => _numberPerSpawn += value;

    public void DecreaseSpawnInterval(float value)
    {
        if (_spawnInterval >= _minSpawnInterval) _spawnInterval -= value;
    }

    public void UpdateCurrentEnemiesRemaining() => CurrentEnemiesRemaining--;

    public void IncreaseCurrentEnemySpeed(float value) => m_currentEnemySpeed += value;
}
