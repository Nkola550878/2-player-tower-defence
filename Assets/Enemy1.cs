using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float damage;
    Enemy enemy;
    bool attacking;

    [SerializeField] float attackTime;
    float timeToNextAttack;
    GameObject tower;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (attacking)
        {
            timeToNextAttack -= Time.deltaTime;
            if(timeToNextAttack < 0)
            {
                timeToNextAttack = attackTime;
                Attack();
            }
        }
    }

    private void Attack()
    {
        if(tower == null)
        {
            enemy.moving = true;
            attacking = false;
            return;
        }
        tower.GetComponent<Building>().TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Tower"))
        {
            attacking = true;
            enemy.moving = false;
            tower = collider.gameObject;
        }
    }


}
