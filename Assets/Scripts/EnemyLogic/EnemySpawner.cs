using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnRadius;

    private bool m_hasSpawned = false;

    private void FixedUpdate()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (m_hasSpawned) return;
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        m_hasSpawned = true;

        Vector3 randomPosition = Random.insideUnitSphere * _spawnRadius;
        randomPosition.y = 1;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position + randomPosition, out hit, _spawnRadius, NavMesh.AllAreas))
        {
            Instantiate(_enemyPrefab, hit.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(_spawnInterval);
        m_hasSpawned = false;
    }
}
