using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private int _maximumNumberEnemies = 3;

    private int _spawnIndex = 0;
    private int _defaultSpawnIndex = 0;
    private List<Enemy> _enemies = new List<Enemy>();
    private bool _canSpawn = true;
    
    private void Start() => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        while (_canSpawn)
        {
            RemoveUnnecessary();
        
            var spawnPosition = GetSpawnPosition();
            var enemy = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            _enemies.Add(enemy);
        
            yield return new WaitForSeconds(_delay);
        }
    }

    private void RemoveUnnecessary()
    {
        if (_enemies.Count >= _maximumNumberEnemies)
        {
            var indexFirstEnemy = 0;
            var enemyToDelete = _enemies[indexFirstEnemy];
            
            _enemies.Remove(enemyToDelete);
            Destroy(enemyToDelete.gameObject);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var spawnPosition = _spawnPositions[_spawnIndex].transform.position;
        var indexEndList = _spawnPositions.Count - 1;
        
        _spawnIndex = _spawnIndex == indexEndList ? _spawnIndex = _defaultSpawnIndex : ++_spawnIndex;
        return spawnPosition;
    }
}