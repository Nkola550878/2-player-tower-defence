using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int price;

    [HideInInspector] public Transform enemyParent;

    [HideInInspector] public Transform currentEnemy;
    float timeToNexBullet;

    private void Start()
    {

    }

    void Update()
    {

    }

    public void CheckEnemy(float radius)
    {
        if (currentEnemy == null || (transform.position - currentEnemy.position).sqrMagnitude > radius * radius)
        {
            currentEnemy = null;
        }
    }

    public void FindEnemy(float radius)
    {
        foreach (Transform enemy in enemyParent)
        {
            if ((transform.position - enemy.position).sqrMagnitude < radius * radius)
            {
                currentEnemy = enemy;
                return;
            }
        }
        currentEnemy = null;
    }
}
