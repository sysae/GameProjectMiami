using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    Vector3 direction;
    CharacterController controller;

    private float speed = 40f;
    private float gravity = 20f;

    [SerializeField]private Equipment equipment;
    private Weapon currentWeapon;
    public float fromLastShot;

    public Text objective;
    public GameObject Finish;

    public GameObject BulletPrefab;

    [SerializeField] private AudioSource AudioSource;
    private AudioSource StartMusic;

    [SerializeField] private float coeff;

    
    public AudioClip PickUpWeaponSound;
    public AudioClip DropWeaponSound;
    [SerializeField] Transform hands;
    [SerializeField] Camera mainCamera;
    [SerializeField] LevelHealth health;
    

    void CubeController()
    {
        //TODO: исправить хуйню
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (controller.isGrounded)
        {
            direction = new Vector3(x, 0f, z);
            direction = transform.TransformDirection(direction) * speed;
        }
        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);

        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 166f, transform.position.z);
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groungPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        UpdateCameraPosition();
        if (groungPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //UnityEngine.Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void UpdateCameraPosition()
    {

        var offset = new Vector3(0, 130, 0);
        var pos = transform.position + offset;
        mainCamera.transform.position = pos;
    }

    private void DropWeapon()
    {
        currentWeapon.ResetPosition();
        currentWeapon = null;
        equipment.SetIsArmed(false);
        PlayDropWeaponSound();
    }

    private void PlayDropWeaponSound()
    {
        AudioSource.PlayOneShot(DropWeaponSound);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        StartMusic = GetComponent<AudioSource>();
        StartMusic.Play();
        controller = GetComponent<CharacterController>();
    }


    private bool CouldShoot()
    {
        if (currentWeapon.IsAutomatic)
        {
            if(Input.GetMouseButton(0))
                return true;
        }
        else if (Input.GetMouseButtonDown(0))
            return true;
        return false;
    }

    private void Update()
    {
        //в старте это работает криво, пришлось эту хрень сюда запихать

        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        int Enemys = list.Length;

        if (Enemys!=0)
        {
          
            objective.text = string.Format("<color=black>Current objective:</color> kill<color=lime> {0} </color><color=red>red guys</color>", Enemys);
        }
        else
        {
            Finish.SetActive(true);
            objective.text = string.Format("<color=black>Current objective:</color><color=lime> go away</color>");
        }
        if (HasWeapon)
        {
            //clickedLastUpdate
            if (fromLastShot > currentWeapon.ShootDelay)
            {

                if (CouldShoot())
                {
                    Shoot();
                    fromLastShot = 0;

                }
            }
            if (Input.GetKey(KeyCode.G))
                DropWeapon();
        }
        
        fromLastShot += Time.deltaTime;
        CubeController();

    }
    bool IsShootKeyPressed => Input.GetMouseButtonDown(0);
    
    bool HasWeapon => currentWeapon != null;
    //стрельба
    private void Shoot()
    {
        //var p = transform.position;
        //p.y = 0.06f;
        //transform.position = p;
        var pos = currentWeapon.BulletsParent.transform.position;
        var rot = transform.rotation;
        var bullet = Instantiate(BulletPrefab, pos, rot);
        bullet.transform.position = pos;
        currentWeapon.PlayShootSound();
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (!HasWeapon && IsReadyToEquip)
        {
            var weapon = collision.gameObject.GetComponent<Weapon>();
            if (weapon != null)
            {
                EquipWeapon(weapon);
            }
        }

        var go = collision.gameObject;
        if (go.tag == "Teleport")
        {

            var teleport = go.GetComponent<NextScene>();
            if(teleport != null)
            {
                teleport.Teleport(transform);
                //OnTeleport(teleport);
            }
        }

        
        var bullet = go.GetComponent<ShootEnemy>();
        if(bullet != null)
        {
            var bulletDamage = 30;
            health.levelHealth -= bulletDamage;
            bullet.DestroyObject();
        }

        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Сaptions");
        }

    }

   
/* private void OnTeleport(NextScene nextLevel)
 {

 }*/

void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        equipment.SetIsArmed(true);

        currentWeapon.SetArmedPosition(hands);
        AudioSource.PlayOneShot(PickUpWeaponSound);
    }

    private bool IsReadyToEquip => Input.GetKey(KeyCode.E);

    //public void SpawnPlayerInPoint(Vector3 position)
    //{
    //    var ch = GetComponent<CharacterController>();
    //    ch.enabled = false;
    //    transform.position = position;
    //    ch.enabled = true;
    //    //ch.SimpleMove();
    //}

    

}
