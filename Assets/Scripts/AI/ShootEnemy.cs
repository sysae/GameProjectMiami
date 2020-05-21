using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootEnemy : MonoBehaviour
{
    public float speed;
    public int Health;
    private Transform player;
    private Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.z == target.z)
        {
            DestroyObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyObject();
            player.GetComponent<LevelHealth>().levelHealth -= Health;
        }
    }
    // Удаляем объект пули
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
