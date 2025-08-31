using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float triggerTime;

    SpriteRenderer spriteRenderer;
    float timeToExplotion = Mathf.Infinity;
    bool mine;

    [SerializeField] float bombDamage = 30f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timeToExplotion -= Time.deltaTime;

        if(timeToExplotion < 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mine)
        {
            if (collision.collider.CompareTag("Unit"))
            {
                Destroy(collision.otherCollider);
                spriteRenderer.enabled = true;
                timeToExplotion = triggerTime;
            }
        }
    }

    public void Explode()
    {
        Collider2D[] colliders = new Collider2D[10];
        ContactFilter2D contactFilter = new ContactFilter2D();
        int count;

        CircleCollider2D[] bombColliders = GetComponents<CircleCollider2D>();
        if (bombColliders[0].isTrigger)
        {
            count = bombColliders[0].OverlapCollider(contactFilter, colliders);
        }
        else
        {
            count = bombColliders[1].OverlapCollider(contactFilter, colliders);
        }

        for (int i = 0; i < count; i++)
        {
            if (colliders[i].GetComponent<Enemy>() != null)
            {
                colliders[i].GetComponent<Enemy>().TakeDamage(bombDamage);
            }
        }

        Destroy(gameObject);
    }
}
