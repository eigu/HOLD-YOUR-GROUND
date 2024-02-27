using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private GameObject _enemyPrefab;
  [SerializeField] private float _spawnInterval;
  [SerializeField] private float _spawnRadius;

  void Start()
  {
    InvokeRepeating("SpawnEnemy", 0f, _spawnInterval);
  }

  void SpawnEnemy()
  {
    Vector3 randomPosition = Random.insideUnitSphere * _spawnRadius;
    randomPosition.y = 1;

    Instantiate(_enemyPrefab, transform.position + randomPosition, Quaternion.identity);
  }
}
