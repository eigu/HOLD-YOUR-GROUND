using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnRadius;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 1;

        Instantiate(enemyPrefab, transform.position + randomPosition, Quaternion.identity);
    }
}
