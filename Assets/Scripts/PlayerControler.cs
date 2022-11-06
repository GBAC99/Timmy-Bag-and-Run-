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

        if (gameManager.actualState == "Play")
        {
            PlayerMove();
        }

        if (staminaCurrentTime > staminaTime) staminaCurrentTime = staminaTime;

        staminaBar.fillAmount = staminaCurrentTime / staminaTime;//deberia hacerlo el gamemanager   
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.actualState == "Play" && !gameManager.fastForward)
        {
            if (ds)
            {
                if (other.gameObject.tag == "Ghost")
                {
                    gameManager.SetGameState("Dead");
                }
            }
            else
            {
                if (other.gameObject.tag == "Ghost"
                    || other.gameObject.tag == "Chain"
                    || other.gameObject.tag == "Axe") gameManager.SetGameState("Dead");
            }
        }

        if (other.gameObject.tag == "PickUp")
        {
            PickUp pickUp = gameObject.GetComponent<PickUp>();
            pickUp.TakePickUp();
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
        PlayerDissapear();

    }
    void PlayerDissapear()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Stamina Managment
            if (staminaCurrentTime > 0)
            {
                ds = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {

            //Stamina Managment
            if (staminaCurrentTime < staminaTime)
            {
                ds = false;
            }
        }

        if (ds && staminaCurrentTime > 0)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            meshRenderer.GetComponent<Renderer>().material.color = Color.white;
            staminaCurrentTime -= staminaSpend * Time.deltaTime;
        }
        else if (ds && staminaCurrentTime <= 0)
        {
            meshRenderer.GetComponent<Renderer>().material.color = Color.gray;
            staminaCurrentTime = 0;
            ds = false;
        }
        else if (!ds && staminaCurrentTime < staminaTime)
        {
            staminaCurrentTime += staminaRecoverSpeed * Time.deltaTime;
            meshRenderer.GetComponent<Renderer>().material.color = Color.gray;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    public void FillStamina(float fill)
    {
        staminaCurrentTime += fill;
    }

    public void Restart()
    {
        ds = false;
        staminaCurrentTime = staminaTime;
        transform.position = initPosition;
    }

}
