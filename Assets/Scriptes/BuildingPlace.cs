using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlace : MonoBehaviour
{
    [SerializeField] GameObject[] buildings;
    [SerializeField] int currentBuildingIndex;

    GameManager gameManager;
    Camera cam;
    Vector3 mousePosition;
    Shop shop;


    void Start()
    {
        cam = FindObjectOfType<Camera>();
        shop = FindObjectOfType<Shop>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
    }
}
