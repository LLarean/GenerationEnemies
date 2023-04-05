using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maximumNumberEnemies;

    private List<Enemy> _enemies = new List<Enemy>();
    private float _runningTime;
    private int _spawnCount;
    private int _defaultSpawnCount = 0;
    
    private void Update()
    {
        _runningTime += Time.deltaTime;
        
        if (_runningTime >= _spawnDelay)
        {
            SpawnEnemy();
            _runningTime = 0;
        }
    }

    private void SpawnEnemy()
    {
        RemoveUnnecessaryEnemies();

        var spawnPosition = _spawnPositions[_spawnCount].transform.position;
        _spawnCount++;

        if (_spawnCount == _spawnPositions.Count)
        {
            _spawnCount = _defaultSpawnCount;
        }

        Enemy enemy = Instantiate(_enemy, spawnPosition, Quaternion.identity);
        _enemies.Add(enemy);
    }

    private void RemoveUnnecessaryEnemies()
    {
        if (_enemies.Count >= _maximumNumberEnemies)
        {
            int firstEnemyCount = 0;
            var enemyToDelete = _enemies[firstEnemyCount];
            
            _enemies.Remove(enemyToDelete);
            Destroy(enemyToDelete);
        }
    }
}
