using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootEnemy : MonoBehaviour
{
    public float speed;
    public int Health;
    private Transform player;
    private Vector3 target;
    private float timeAlive;
    [SerializeField] private float maxTimeAlive;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > maxTimeAlive)
            DestroyObject();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print(collision.gameObject.name);
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        DestroyObject();
    //        player.GetComponent<LevelHealth>().levelHealth -= Health;
    //    }
    //}

    // Удаляем объект пули
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
