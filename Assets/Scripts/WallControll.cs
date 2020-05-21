using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControll : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var wallObject = "Wall";
        if (collision.gameObject.tag == (wallObject))
        {
            Destroy(gameObject);
        }
    }
}
