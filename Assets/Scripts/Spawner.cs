using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager gameManager;

    public Transform[] spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddToList(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateObstacle(string obstacle, int spawner)
    {
        switch (obstacle)
        {
            case "Ghost":
                Instantiate(gameManager.ReturnObject(obstacle), spawnPositions[spawner], true);
                break;
            case "Chain":
                Instantiate(gameManager.ReturnObject(obstacle), spawnPositions[spawner], true);
                break;
            case "Axe":
                Instantiate(gameManager.ReturnObject(obstacle), spawnPositions[spawner], true);
                break;
            case "PotiG":
                Instantiate(gameManager.ReturnObject(obstacle), spawnPositions[spawner], true);
                break;
            case "PotiB":
                Instantiate(gameManager.ReturnObject(obstacle), spawnPositions[spawner], true);
                break;
            case "PotiR":
                Instantiate(gameManager.ReturnObject(obstacle), spawnPositions[spawner], true);
                break;
        }
    }

}
