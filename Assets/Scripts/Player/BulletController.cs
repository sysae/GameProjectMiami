using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 3500);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Wall")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        


    }
}

