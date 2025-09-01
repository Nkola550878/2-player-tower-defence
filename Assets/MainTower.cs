using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTower : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Text hpText;
    [SerializeField] GameObject gameOver;
    [SerializeField] Canvas canvas;

    GameManager gameManager;

    private void Start()
    {
        hpText.text = $"HP: {health}";
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Hit()
    {
        health -= 1;
        hpText.text = health.ToString();

        if(health < 0)
        {
            Time.timeScale = 0;
            GameObject instance = Instantiate(gameOver, Vector3.zero, Quaternion.identity, canvas.transform);
            instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(gameManager.Restart);
        } 
    }
}
