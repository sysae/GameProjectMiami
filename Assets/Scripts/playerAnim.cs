using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Animator anim;

    public void SetMotion(float x, float y)
    {
        anim.SetFloat("vertical", x);
        anim.SetFloat("horizontal", y);
    }
}
