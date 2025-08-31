using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int price;

    [SerializeField] float speed;
    [SerializeField] float health;

    [HideInInspector] public Transform locationParent;
    Vector3[] postions;
    Rigidbody2D rigidbody2D;
    int nextPositionIndex = 0;
    public bool moving = true;

    void Start()
    {
        postions = new Vector3[locationParent.childCount];
        rigidbody2D = GetComponent<Rigidbody2D>();

        for (int i = 0; i < locationParent.childCount; i++)
        {
            postions[i] = locationParent.GetChild(i).transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rigidbody2D.MovePosition(Vector3.MoveTowards(transform.position, postions[nextPositionIndex], speed * Time.fixedDeltaTime));
        }
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, postions[nextPositionIndex], speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, postions[nextPositionIndex]) <= speed * Time.deltaTime)
        {
            nextPositionIndex++;
        }

        if(nextPositionIndex == postions.Count())
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Bullet>() != null)
        {
            TakeDamage(collision.collider.GetComponent<Bullet>().damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
