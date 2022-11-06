using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Lista de todos los objetos del juego
    List<GameObject> sceneObjects = new List<GameObject>();

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

    public bool fastForward;

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

        actualGameSpeed = startGameSpeed;

        fastForward = false;

        mainCanvasUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        GameStates(actualState);

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetGameState("Dead");
        }

        if (Input.GetKey(KeyCode.P))
        {
            actualGameSpeed = 10f;
        fastForward = true;

        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            fastForward = false;

            actualGameSpeed = startGameSpeed;
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
                mainCanvasUI.SetActive(true);
                Time.timeScale = 1;
                actualGameSpeed = startGameSpeed;
                SetGameState("Play");
                break;
            case "Dead":
                actualGameSpeed = 0; //Pause all objects
                mainCanvasUI.SetActive(false);
                mainDeadScreenUI.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }


    public void AddToList(GameObject g)
    {
        sceneObjects.Add(g);
    }

    public void RemoveFromList(GameObject g)
    {
        sceneObjects.Remove(g);
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
