using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Lista de todos los objetos del juego
    List<GameObject> sceneObjects = new List<GameObject>();

    //Lista de Objetos Spawneables
    public GameObject[] spawnableObjects;

    //Referencia al Canvas
    public GameObject mainCanvasUI;
    public GameObject mainDeadScreenUI;

    //Referencia directa al Player
    public PlayerControler player;

    //Propiedades del control del juego
    public float startGameSpeed;
    [HideInInspector]
    public float actualGameSpeed;

    public string actualState;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Movable").AddComponent<GameManager>();
            }

            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnableObjects.Length; i++)
        {
            AddToList(spawnableObjects[i]);
        }

        player.SetState("Play");

        actualGameSpeed = startGameSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        GameStates(actualState);

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetGameState("Dead");
        }


        if (Input.GetKeyDown(KeyCode.P))
        {

        }
        else if (Input.GetKeyUp(KeyCode.P))
        {

        }

    }


    public void SetGameState(string state)
    {
        actualState = state;
    }

    void GameStates(string state)
    {
        switch (state)
        {
            case "Play":
                actualGameSpeed = startGameSpeed;
                player.SetState("Play");

                break;
            case "Restart":
                foreach (GameObject o in sceneObjects)
                {
                    if (o.gameObject.GetComponent<Movable>())
                    {
                        o.GetComponent<Movable>().Restart();
                    }
                }

                player.Restart();

                mainDeadScreenUI.SetActive(false);
                Time.timeScale = 1;
                SetGameState("Play");

                break;
            case "Dead":
                actualGameSpeed = 0; //Pause all objects
                mainDeadScreenUI.SetActive(true);
                Time.timeScale = 0;
                //player.SetState("Dead");
                break;
        }
    }


    public void AddToList(GameObject g)
    {
        sceneObjects.Add(g);
    }

    public GameObject ReturnObject(string tagToFind)
    {
        GameObject rObj =  null;

        foreach (GameObject g in sceneObjects)
        {
            if (g.tag == tagToFind)
            {
                rObj = g;
            }
        }

        return rObj;
    }

}
