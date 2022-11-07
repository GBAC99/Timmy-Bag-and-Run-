using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Lista de todos los objetos del juego
    List<GameObject> sceneObjects = new List<GameObject>();

    //Referencia al Canvas
    public GameObject mainCanvasUI;
    public GameObject mainDeadScreenUI;

    public TutorialControler tutorialControler;

    //Referencia directa al Player
    public PlayerControler player;

    //Propiedades del control del juego
    public float startGameSpeed;
    [HideInInspector]
    public float actualGameSpeed;
    public string actualState;
    
    public bool fastForward;
    public bool slowDown;
    public float slowTime;
    public float slowCurrentTime;
    public float slowTimeSpend;

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

        slowCurrentTime = 0;

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

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
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
              
                if (slowDown)
                {

                    slowCurrentTime += slowTimeSpend * Time.deltaTime;
                    if (slowCurrentTime >= slowTime)
                    {
                        slowCurrentTime = 0;
                        slowTime = 0;
                        actualGameSpeed = startGameSpeed;
                        slowDown = false;

                    }
                }
                else
                {
                    actualGameSpeed = startGameSpeed;
                }
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

    public void SlowGame(float _slowTime, float slowSpeed)
    {
        slowDown = true;
        slowTime = _slowTime;
        actualGameSpeed = slowSpeed;
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
