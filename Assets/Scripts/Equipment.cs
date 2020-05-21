using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private bool isArmed;

    [SerializeField] private Animator anim;
    const int ArmedLayerIndex = 1;

    public bool IsArmed => isArmed;

    public void SetIsArmed(bool armed)
    {
        isArmed = armed;
        anim.SetBool("Armed", armed);
        SetArmedLayerState(armed);
    }

    private void SetArmedLayerState(bool state)
    {
        var weight = state ? 1 : 0;
        anim.SetLayerWeight(ArmedLayerIndex, weight);
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
