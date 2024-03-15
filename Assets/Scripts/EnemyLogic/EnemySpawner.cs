using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _rampUpRate;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _minPlayerDistance;

    private float m_nextSpawnTime;

    private void Start()
    {
        m_nextSpawnTime = Time.time + _spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= m_nextSpawnTime)
        {
            Vector3 randomSpawnPos = FindValidSpawnPosition();
            if (randomSpawnPos != Vector3.zero)
            {
                Instantiate(_enemyPrefab, randomSpawnPos, Quaternion.identity);
            }

            _spawnInterval *= _rampUpRate;
            _spawnInterval = Mathf.Max(_spawnInterval, _minSpawnInterval);

            m_nextSpawnTime = Time.time + _spawnInterval;
        }
    }

    private Vector3 FindValidSpawnPosition()
    {
        Vector3 randomSpawnPos = Vector3.zero;
        int attempts = 0;
        while (attempts < 10)
        {
            randomSpawnPos = RandomNavMeshPosition(transform.position, _spawnRadius);
            if (randomSpawnPos != Vector3.zero && Vector3.Distance(randomSpawnPos, _player.position) >= _minPlayerDistance)
            {
                return randomSpawnPos;
            }
            attempts++;
        }
        return Vector3.zero;
    }

    private Vector3 RandomNavMeshPosition(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}
