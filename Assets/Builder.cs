using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] GameObject[] buildings;

    int index;
    Camera cam;
    Shop shop;
    Vector3 mousePosition;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        shop = transform.parent.GetChild(0).GetComponent<Shop>();
    }

    void Update()
    {
        if (GameManager.started)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            return;
        }

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward * 20f);
        foreach (Transform buildingPlace in transform.GetChild(0))
        {
            if (hit.collider != null && hit.collider.transform == buildingPlace && buildingPlace.GetComponent<BuildingPlace>() != null)
            {
                if (shop.CanBuy(buildings[index].GetComponent<Building>().price))
                {
                    shop.Buy(buildings[index].GetComponent<Building>().price);
                    Instantiate(buildings[index], buildingPlace.position, Quaternion.identity, transform.GetChild(0));
                    Destroy(buildingPlace.gameObject);
                }
            }
        }
    }

    public void SelectBuilding(int index)
    {
        this.index = index;
    }
}
