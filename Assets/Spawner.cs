using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies = new GameObject[1];
    [SerializeField] float timeBetweenEnemies;
    public Transform enemyParent;
    
    Shop shop;
    Queue<int> enemyesForNextWave = new Queue<int>();
    float timeToNextEnemy;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        shop = transform.parent.GetChild(0).GetComponent<Shop>();
    }

    public void AddNextEnemy(int index)
    {
        if (GameManager.started) return;

        if (shop.CanBuy(enemies[index].GetComponent<Enemy>().price))
        {
            enemyesForNextWave.Enqueue(index);
            //Instantiate(enemies[index], transform.position, Quaternion.identity);
            //shop.money -= enemies[index].GetComponent<Enemy>().price;
            shop.Buy(enemies[index].GetComponent<Enemy>().price);
        }
    }

    private void Update()
    {
        if (!GameManager.started)
        {
            timeToNextEnemy = -1f;
        }

        timeToNextEnemy -= Time.deltaTime;

        if(timeToNextEnemy < 0f && GameManager.started)
        {
            if(enemyesForNextWave.Count > 0)
            {
                Spawn(enemyesForNextWave.Dequeue());
            }
            timeToNextEnemy = timeBetweenEnemies;
        }
    }

    private void Spawn(int index)
    {
        GameObject instance = Instantiate(enemies[index], transform.position, Quaternion.identity, enemyParent);
        instance.GetComponent<Enemy>().locationParent = transform;
    }
}