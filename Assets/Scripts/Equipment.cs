using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private bool isArmed;

    [SerializeField] private Animator anim;

    public bool IsArmed => isArmed;

    public void SetIsArmed(bool armed)
    {
        isArmed = armed;
    }

    public void Update()
    {
        var x = Input.GetAxis("Vertical");
        var y = Input.GetAxis("Horizontal");
        SetMotion(x, y);
    }

    public void SetMotion(float x, float y)
    {
        anim.SetFloat("vertical", x);
        anim.SetFloat("horizontal", y);
    }
}
