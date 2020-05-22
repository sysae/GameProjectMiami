using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool isAutomatic;
    [SerializeField] private float rateOfFire = 0.5f;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private Transform bulletsParent;

    private Rigidbody rb;

    public bool IsAutomatic => isAutomatic;
    public float ShootDelay => rateOfFire;
    public Transform BulletsParent => bulletsParent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void PlayShootSound()
    {
        shootSound.PlayOneShot(shootSound.clip);
        //shootSound.Play();
        //AudioSource.PlayOneShot(shootSound.clip);
    }


    public void SetArmedPosition(Transform hands)
    {
        transform.parent = hands;
        transform.localPosition = Vector3.zero;
        //weapon.transform.localPosition = new Vector3(0.028f, 5.11f, 0.766f);
        transform.localRotation = Quaternion.identity;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void ResetPosition()
    {
        transform.parent = null;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(transform.forward * 500);
    }
}
