using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool isAutomatic;
    public bool IsAutomatic => isAutomatic;

}
