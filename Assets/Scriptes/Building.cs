using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public int price;

    [SerializeField] float health;
    [SerializeField] GameObject buildingUI;
    [SerializeField] GameObject buildingPlace;

    public Transform enemyParent;

    public Transform currentEnemy;
    float timeToNexBullet;
    Camera cam;
    GameManager gameManager;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<GameManager>();

        if (enemyParent == null)
        {
            if (gameManager.player1Manager.transform.GetChild(2).GetChild(0) == transform.parent)
            {
                enemyParent = gameManager.player2Manager.transform.GetChild(3);
                return;
            }
            enemyParent = gameManager.player1Manager.transform.GetChild(3);

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 20f);

            if (hit && hit.collider.transform == transform)
            {
                GameObject instance = Instantiate(buildingUI, transform.position, Quaternion.identity, transform);
                instance.GetComponent<Canvas>().worldCamera = cam;
                instance.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Upgrade);
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Delete);
            }
        }
    }

    void Upgrade()
    {
        Debug.Log("Upgrade");
    }

    void Delete()
    {
        transform.parent.parent.parent.GetChild(0).GetComponent<Shop>().money += 40;
        Instantiate(buildingPlace, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    public void CheckEnemy(float radius)
    {
        if (currentEnemy == null || (transform.position - currentEnemy.position).sqrMagnitude > radius * radius)
        {
            currentEnemy = null;
        }
    }

    public void FindEnemy(float radius)
    {
        foreach (Transform enemy in enemyParent)
        {
            if ((transform.position - enemy.position).sqrMagnitude < radius * radius)
            {
                currentEnemy = enemy;
                return;
            }
        }
        currentEnemy = null;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Delete();
        }
    }
}
