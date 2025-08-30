using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building1 : Building
{
    [SerializeField] GameObject bullet;
    [SerializeField] float buildingRadius = 3f;
    [SerializeField] float timeBetweenBullets;
    [SerializeField] GameObject buildingUI;
    [SerializeField] GameObject buildingPlace;

    GameManager gameManager;
    float timeToNexBullet;
    Camera cam;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<GameManager>();

        if(enemyParent == null)
        {
            if(gameManager.player1Manager.transform.GetChild(2).GetChild(0) == transform.parent)
            {
                Debug.Log("a");
                enemyParent = gameManager.player2Manager.transform.GetChild(3);
                return;
            }
            enemyParent = gameManager.player1Manager.transform.GetChild(3);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemy(buildingRadius);
        if(currentEnemy == null)
        {
            FindEnemy(buildingRadius);
        }

        timeToNexBullet -= Time.deltaTime;

        if (timeToNexBullet < 0f)
        {
            if (currentEnemy != null)
            {
                GameObject lastBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                lastBullet.GetComponent<Bullet>().direction = transform.position - currentEnemy.position;
                lastBullet.GetComponent<Bullet>().radius = buildingRadius;
            }
            timeToNexBullet = timeBetweenBullets;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 20f);

            if (hit.collider.transform == transform)
            {
                Debug.Log("a");
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
        Instantiate(buildingPlace, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
