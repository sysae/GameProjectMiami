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

    private float speed = 20f;
    private float gravity = 20f;

    [SerializeField]private Equipment equipment;
    private Weapon currentWeapon;
    public float fromLastShot;

    public GameObject BulletPrefab;

    [SerializeField] private AudioSource AudioSource;
    private AudioSource StartMusic;

    public AudioClip PickUpWeaponSound;
    public AudioClip DropWeaponSound;
    [SerializeField] Transform hands;

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
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
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
    }

    void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        equipment.SetIsArmed(true);

        currentWeapon.SetArmedPosition(hands);
        AudioSource.PlayOneShot(PickUpWeaponSound);
    }

    private bool IsReadyToEquip => Input.GetKey(KeyCode.E);
}
