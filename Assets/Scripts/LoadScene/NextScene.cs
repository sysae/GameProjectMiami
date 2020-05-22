using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    
    [SerializeField] private Transform pointPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var player = other.gameObject.GetComponent<Cube>();
            if(player != null)
            {
                //player.SpawnPlayerInPoint(pointPos.position);
            }

        }
    }

    internal void Teleport(Transform transform)
    {
        transform.position = pointPos.position;
    }
}
