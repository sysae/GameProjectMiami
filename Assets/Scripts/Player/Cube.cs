using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Vector3 direction;
    CharacterController controller;
    public float speed = 15f;
    public float gravity = 20f;

    public GameObject cube;
    public GameObject BulletsParent;
    public GameObject Bullet;
    private GameObject Object;


    private bool isEquip = false;

    void CubeController()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (controller.isGrounded)
        {
            direction = new Vector3(x, 0f, z);
            direction = transform.TransformDirection(direction) * speed;
        }
        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
        Camera.main.transform.position = new Vector3(cube.transform.position.x, Camera.main.transform.position.y,cube.transform.position.z);
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groungPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groungPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //UnityEngine.Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void Shoot()
    {
        //BulletsParentPosition = BulletsParent.transform.position;
        Bullet.transform.position = BulletsParent.transform.position;
        Bullet.transform.rotation = cube.transform.rotation;
        print("SHOOOOOOOOOOOOOOOOOOOOOT");
        Instantiate(Bullet);
    }

    void DropWeapon()
    {
        Object.transform.parent = null;
        Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Object.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
        isEquip = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CubeController();
        //типа пистолет
        if (Input.GetMouseButtonDown(0))
        {
            if (isEquip)
            {
                Shoot();
            }
        }

        if(Input.GetKey(KeyCode.G) & isEquip)
        {
            DropWeapon();
        }

        //типа автомат
        //if (Input.GetMouseButton(1))
        //{
        //    Shoot();
        //}
    }


    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.tag == "Equipment")
        {
            Object = collision.gameObject;
            Object.transform.parent = cube.transform;
            Object.gameObject.transform.localPosition = new Vector3(0.028f, -0.11f, 0.766f);
            Object.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Object.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isEquip = true;
        }
    }
}
