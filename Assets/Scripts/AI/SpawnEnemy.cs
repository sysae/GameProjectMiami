using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
     public GameObject[] _enemyPrefab;
     public GameObject[] _pointTarget;

    [SerializeField] private Cube player;

    void Start()
    {
        SpawnRandomEnemy();
    }

    public void SpawnRandomEnemy()
    {
        
        for(int i = 0; i <_pointTarget.Length -1; i++)
        {
            var go = Instantiate(_enemyPrefab[i], _pointTarget[i].transform.position, Quaternion.identity);
        }
        
    }
}
