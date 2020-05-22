using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyColor : MonoBehaviour
{
    [SerializeField] Renderer enemyRenderer;
    [SerializeField] Color aliveColor;
    [SerializeField] Color deadColor;
    void Start()
    {
        SetColor(aliveColor);
    }

    public void SetColor(Color color)
    {
        enemyRenderer.material.DOColor(color, 1f);
    }

    public void Die()
    {
        GetComponent<AIEnemy>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        SetColor(deadColor);
    }
}
