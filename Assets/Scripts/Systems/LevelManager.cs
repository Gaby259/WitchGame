using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private GameObject _troll;
    private int _spawnedEnemies;
    [SerializeField] private int _totalEnemies;

    [SerializeField] private Transform[] _spawnPoints;
    private IState _currentState;
    
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
