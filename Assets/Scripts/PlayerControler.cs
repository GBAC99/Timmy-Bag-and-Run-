using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    GameManager gameManager;

    public Material meshRenderer;

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
         
        if (extraStamina)
        {
            staminaBarExtra.fillAmount = staminaCurrentTimeExtra / staminaTimeExtra;
        }
        else
        {
            staminaBarExtra.fillAmount = 0;
            staminaBar.fillAmount = staminaCurrentTime / staminaTime;  
        }

        if (invencible)
        {
            meshRenderer.color = Color.red;

            invencibleCurrentTime += invencibleTimeSpend * Time.deltaTime;
            Debug.Log(invencibleCurrentTime);
            if (invencibleCurrentTime >= invencibleTime)
            {
                invencibleCurrentTime = 0;
                meshRenderer.color = Color.gray;
                invencible = false;

            }

        }


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
            gameManager.LoadNextScene();
        }


        if (other.gameObject.tag == "Tutorial")
        {
            Debug.Log("tuto");
            gameManager.tutorialControler.SetTutorialUP(other.gameObject.GetComponent<TutorialVolume>().num);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tutorial")
        {
            gameManager.tutorialControler.SetTutorialOFF(other.gameObject.GetComponent<TutorialVolume>().num);
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
                meshRenderer.color = Color.white;
                staminaCurrentTimeExtra -= staminaSpendExtra * Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                meshRenderer.color = Color.gray;
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
                
                if (staminaCurrentTime > 0)
                {
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    meshRenderer.color = Color.white;
                    staminaCurrentTime -= staminaSpend * Time.deltaTime;
                    ds = true;

                }
                else
                {
                    gameObject.GetComponent<BoxCollider>().isTrigger = false;
                    meshRenderer.color = Color.gray;
                    staminaCurrentTime += staminaRecoverSpeed * Time.deltaTime;
                    staminaCurrentTime = 0;
                    ds = false  ;

                }



            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                ds = false;
            }
            else if (!ds && staminaCurrentTime < staminaTime)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                meshRenderer.color = Color.gray;
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
