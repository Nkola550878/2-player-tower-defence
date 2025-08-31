using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float radius;
    public float damage;

    [SerializeField] bool justDamage;
    [HideInInspector] public Vector3 direction;

    void Start()
    {
        Destroy(gameObject, radius / speed);
    }

    void Update()
    {
        transform.position -= direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (justDamage)
        {
            if(collision.collider.GetComponent<Enemy>() != null)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            GetComponent<Bomb>().Explode();
        }
    }
}
