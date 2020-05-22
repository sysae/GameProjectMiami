using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAtEnemies : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        var bulletTag = "Bullet";
        if(other.gameObject.tag == (bulletTag))
        {
           Destroy(gameObject);
        }
    }
}
