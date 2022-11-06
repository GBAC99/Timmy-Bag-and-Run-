using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    GameManager gameManager;

    public GameObject meshRenderer;

    public Image staminaBar;
    public float staminaTime;
    public float staminaSpend;
    public float staminaRecoverSpeed;
    float staminaCurrentTime;


    public Transform[] Carriles;
    Vector3 initPosition;

    public float moveSpeed;

    public string actualState;

    public bool ds;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddToList(gameObject);
        initPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        ds = false;
        moveSpeed = 5;
        staminaCurrentTime = staminaTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStates(actualState);
        staminaBar.fillAmount = staminaCurrentTime / staminaTime;//deberia hacerlo el gamemanager   
    }

    public void SetState(string state)
    {
        actualState = state;
    }

    void PlayerStates(string state)
    {
        switch (state)
        {
            case "Play":
                PlayerMove();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Stamina Managment
                    if (staminaCurrentTime > 0)
                    {
                        meshRenderer.GetComponent<Renderer>().material.color = Color.white;
                        ds = true;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    meshRenderer.GetComponent<Renderer>().material.color = Color.gray;

                    //Stamina Managment
                    if (staminaCurrentTime < staminaTime)
                    {
                        ds = false;
                    }
                }

                if (ds)
                {
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    staminaCurrentTime -= staminaSpend * Time.deltaTime;
                }
                else if (!ds && staminaCurrentTime < staminaTime)
                {
                    staminaCurrentTime += staminaRecoverSpeed * Time.deltaTime;
                    gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                break;
            case "Dead":
                gameManager.SetGameState("Dead");
                break;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager.actualState == "Play")
        {
            if (!ds)
            {
                if (collision.gameObject.tag == "Ghost" 
                    || collision.gameObject.tag == "Chain" 
                    || collision.gameObject.tag == "Axe") gameManager.SetGameState("Dead");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ds)
        {
            if (other.gameObject.tag == "Ghost")
            {
                gameManager.SetGameState("Dead");
            }
        }
    }


    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, Carriles[0].position, moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, Carriles[1].position, moveSpeed * Time.deltaTime);
        }
    }

    public void Restart()
    {
        staminaCurrentTime = staminaTime;
        transform.position = initPosition;
    }

}
