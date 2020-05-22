using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
     public GameObject enemyPrefab;
     public GameObject[] pointTarget;

    void Start()
    {
        SpawnRandomEnemy();
    }

    public void SpawnRandomEnemy()
    {
        foreach(var target in pointTarget)
        {
            var go = Instantiate(enemyPrefab, target.transform.position, Quaternion.identity);
        }
    }
}
