using System;
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


    //пушки
    private float RateOfFire = 0.5f;
    private bool isEquip = false;
    private bool IsAutomatic = false;

    //объекты
    public GameObject cube;
    public GameObject BulletsParent;
    public GameObject Bullet;
    private GameObject Object;

    //звуки
    private AudioSource AudioSource;
    private AudioClip ShootSound;
    public AudioSource StartMusic;
    public AudioClip PickUpWeaponSound;
    public AudioClip DropWeaponSound;


   

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

    //стрельба
    void Shoot()
    {       
        if (RateOfFire < 0 & isEquip)
        {
            //BulletsParentPosition = BulletsParent.transform.position;
            Bullet.transform.position = BulletsParent.transform.position;
            Bullet.transform.rotation = cube.transform.rotation;
            Instantiate(Bullet);
            AudioSource.PlayOneShot(ShootSound);
            if (IsAutomatic)
            {
                RateOfFire = 0.15f;
            }
            else
            {
                RateOfFire = 0.1f;
            }
        }          
    }

    //выбросить пушку
    void DropWeapon()
    {
        if (isEquip)
        {
            Object.transform.parent = null;        
            Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Object.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            AudioSource.PlayOneShot(DropWeaponSound);
            isEquip = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartMusic = GetComponent<AudioSource>();
        StartMusic.Play();
        controller = GetComponent<CharacterController>();
    }

    
    // Update is called once per frame
    void Update()
    {
        //просчитываем скорострельнот
        if(RateOfFire > 0)
        {
            RateOfFire -= Time.deltaTime;
            var seconds = RateOfFire % 60;
        }
       
        CubeController();

        if (isEquip)
        {
            if (!IsAutomatic)
            {
                //типа пистолет
                if (Input.GetMouseButtonDown(0))
                {
                    {
                        Shoot();
                    }
                }
            }
            else
            {
                //типа автомат
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }
        }
          
        //выбрасывание пушки
        if (Input.GetKey(KeyCode.G) & isEquip)
        {
            DropWeapon();
        }
    }


    void OnControllerColliderHit(ControllerColliderHit collision)
    {   
        //поднятие пушки
        if (collision.gameObject.tag == "Equipment" & Input.GetKey(KeyCode.E) & !isEquip)
        {
            Object = collision.gameObject;
            var weapon = Object.GetComponent<Weapon>();
            if(weapon != null)
            {
                IsAutomatic = weapon.IsAutomatic;
            }
            //isAutomatic = Convert.ToBoolean(Object.transform.GetChild(0).transform.GetChild(0).name);
            AudioSource = Object.GetComponent<AudioSource>();
            ShootSound = AudioSource.clip;
            Object.transform.parent = cube.transform;
            //исправить хуйню
            Object.gameObject.transform.localPosition = new Vector3(0.028f, -0.11f, 0.766f);
            Object.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Object.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            AudioSource.PlayOneShot(PickUpWeaponSound);
            isEquip = true;
        }
    }
}
