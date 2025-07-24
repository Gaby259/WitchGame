using System;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private GameObject _troll;
    //private int _killedEnemies = 0;
    private int _spawnedEnemies;
    [SerializeField] private int _totalEnemies;

    [SerializeField] private Transform[] _spawnPoints;

    //To-Do
    //Fix waypoint null references on enemies
    
    private void Start()
    {
        _spawnedEnemies = _totalEnemies;
        SpawnEnemies(_spawnedEnemies);
    }

    public void SpawnEnemies(int amount)
    {
        int spawnIndex = 0;
        
        for (int i = 0; i < amount; i++)
        {
            if (spawnIndex > _spawnPoints.Length - 1)
            {
                spawnIndex = 0;
            }
            else
            {
                Instantiate(_zombie, _spawnPoints[spawnIndex].position, Quaternion.identity);
                spawnIndex++;
            }
        }
    }
}
