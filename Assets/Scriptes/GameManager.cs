using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int wave;
    [SerializeField] float timeBetweenWaves = 20f;
    [SerializeField] int moneyPerWave;

    [SerializeField] Text timeText1;
    [SerializeField] Text timeText2;

    public GameObject player1Manager;
    public GameObject player2Manager;


    Shop[] shop;
    float timeToNextWave;
    public static bool started;

    void StartWave()
    {
        started = true;
        shop = FindObjectsOfType<Shop>();
    }

    void Update()
    {
        if(FindObjectOfType<Enemy>() == null && started == true)
        {
            timeToNextWave = timeBetweenWaves;
            foreach (Shop shop in shop)
            {
                shop.money += moneyPerWave;
            }
            wave++;
            started = false;
        }

        if(!started)
        {
            timeToNextWave -= Time.deltaTime;
        }

        if(timeToNextWave < 0)
        {
            StartWave();
        }

        if (!started)
        {
            timeText1.text = timeToNextWave.ToString("0.00");
            timeText2.text = timeToNextWave.ToString("0.00");
        }
    }
}
