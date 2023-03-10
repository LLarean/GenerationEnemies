using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _maximumNumberEnemies;

    private List<GameObject> _enemies = new List<GameObject>();
    private float _runningTime;
    private int _spawnCount = 0;
    
    private void Update()
    {
        _runningTime += Time.deltaTime;

        if (_runningTime >= _spawnTime)
        {
            RemoveUnnecessaryEnemies();
            
            var position = _spawnPositions[_spawnCount].transform.position;
            _spawnCount++;

            if (_spawnCount == _spawnPositions.Count)
            {
                _spawnCount = 0;
            }
            
            GameObject enemy = Instantiate(_enemy, position, Quaternion.identity);
            _enemies.Add(enemy);
            _runningTime = 0;
        }
    }

    private void RemoveUnnecessaryEnemies()
    {
        if (_enemies.Count >= _maximumNumberEnemies)
        {
            var enemyForDeliting = _enemies[0];
            _enemies.Remove(enemyForDeliting);
            Destroy(_enemies[0].gameObject);
        }
    }
}
