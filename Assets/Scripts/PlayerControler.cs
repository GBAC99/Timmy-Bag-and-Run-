using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    GameManager gameManager;


    public Transform[] Carriles;
    public int actualPosition;

    public float moveSpeed;

    public string actualState;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddToList(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerStates(actualState);
        
    }

    public void SetState(string state)
    {
        actualState = state;
    }

    void PlayerStates(string state)
    {
        switch (state)
        {
            case "Play" :
                PlayerMove();
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
            if (collision.gameObject.tag == "Obstacle")
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




        //transform.position = Carriles[actualPosition].transform.position;
    }

}
