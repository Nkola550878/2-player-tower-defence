using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building1 : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float buildingRadius = 3f;
    [SerializeField] float timeBetweenBullets;

    GameManager gameManager;
    float timeToNexBullet;
    Building building;

    void Start()
    {
        building = GetComponent<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.started) return;

        building.CheckEnemy(buildingRadius);
        if(building.currentEnemy == null)
        {
            building.FindEnemy(buildingRadius);
        }

        timeToNexBullet -= Time.deltaTime;

        if (timeToNexBullet < 0f)
        {
            if (building.currentEnemy != null)
            {
                GameObject lastBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                lastBullet.GetComponent<Bullet>().direction = transform.position - building.currentEnemy.position;
                lastBullet.GetComponent<Bullet>().radius = buildingRadius;
            }
            timeToNexBullet = timeBetweenBullets;
        }
    }
}
