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

    public Image staminaBarExtra;
    public float staminaTimeExtra;
    public float staminaSpendExtra;
    public float staminaRecoverSpeedExtra;
    float staminaCurrentTimeExtra;
    public bool extraStamina;

    public Transform[] Carriles;
    Vector3 initPosition;

    public float moveSpeed;

    public string actualState;

    public bool ds;
    public bool invencible;
    public float invencibleTime;
    float invencibleCurrentTime;
    public float invencibleTimeSpend;

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
        extraStamina = false;
        moveSpeed = 5;
        staminaCurrentTime = staminaTime;
        staminaTimeExtra = 0;
        staminaCurrentTimeExtra = staminaTimeExtra;
        invencibleCurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.actualState == "Play")
        {
            PlayerMove();
        }

        // if (staminaCurrentTime > staminaTime) staminaCurrentTime = staminaTime;
        if (extraStamina)
        {
            staminaBarExtra.fillAmount = staminaCurrentTimeExtra / staminaTimeExtra;
        }
        else
        {
            staminaBarExtra.fillAmount = 0;
            staminaBar.fillAmount = staminaCurrentTime / staminaTime;//deberia hacerlo el gamemanager   
        }

        if (invencible)
        {
            meshRenderer.GetComponent<Renderer>().material.color = Color.red;

            invencibleCurrentTime += invencibleTimeSpend * Time.deltaTime;
            Debug.Log(invencibleCurrentTime);
            if (invencibleCurrentTime >= invencibleTime)
            {
                invencibleCurrentTime = 0;
                meshRenderer.GetComponent<Renderer>().material.color = Color.gray;
                invencible = false;

            }

        }


    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.actualState == "Play" && !gameManager.fastForward)
        {
            if (!invencible)
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

            


        }

        if (other.gameObject.tag == "PickUp")
        {
            PickUp pickUp = other.gameObject.GetComponent<PickUp>();
            pickUp.TakePickUp();
        }
        if (other.gameObject.tag == "Meta")
        {
            Debug.Log("MAUJAUAJUJUAJ");
            gameManager.LoadNextScene();
        }
    }

    public void SetPlayerInvencible()
    {
        invencible = true;
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

        if (extraStamina)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                meshRenderer.GetComponent<Renderer>().material.color = Color.white;
                staminaCurrentTimeExtra -= staminaSpendExtra * Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                meshRenderer.GetComponent<Renderer>().material.color = Color.gray;
            }

            if (staminaCurrentTimeExtra <= 0)
            {
                extraStamina = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                meshRenderer.GetComponent<Renderer>().material.color = Color.white;
                if (staminaCurrentTime > 0)
                {
                    staminaCurrentTime -= staminaSpend * Time.deltaTime;
                    ds = true;
                }
                else
                {
                    staminaCurrentTime = 0;
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                ds = false;
            }
            else if (!ds && staminaCurrentTime < staminaTime)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                meshRenderer.GetComponent<Renderer>().material.color = Color.gray;
                staminaCurrentTime += staminaRecoverSpeed * Time.deltaTime;
            }
        }

    }

    public void FillStamina(float fill)
    {
        if (staminaCurrentTime < staminaTime)
        {
            staminaCurrentTime += fill;
        }
        else if (staminaCurrentTime >= staminaTime)
        {
            staminaTimeExtra = fill;
            staminaCurrentTimeExtra = staminaTimeExtra;
            extraStamina = true;
        }

    }

    public void Restart()
    {
        ds = false;
        staminaCurrentTime = staminaTime;
        transform.position = initPosition;
    }

}
