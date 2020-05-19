using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
     public GameObject[] _enemyPrefab;
     public GameObject[] _pointTarget;


    void Start()
    {
        testSpawn();
    }

    public void testSpawn()
    {
        
        for(int i = 0; i <_pointTarget.Length -1; i++)
        {
            Instantiate(_enemyPrefab[i], _pointTarget[i].transform.position, Quaternion.identity);
        }
        
    }
}
