using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public Transform[] Carriles;
    public int actualPosition;   

    // Start is called before the first frame update
    void Start()
    {
        actualPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.A) && ((actualPosition <= Carriles.Length) && (actualPosition > 0)))
        {
            actualPosition -= 1;
        }
        else
        {
            actualPosition = actualPosition + 0;
        }

        if (Input.GetKeyDown(KeyCode.D) && ((actualPosition >= 0) && (actualPosition < Carriles.Length-1)))
        {
            actualPosition += 1;
        }
        else
        {
            actualPosition = actualPosition + 0;
        }

        transform.position = Carriles[actualPosition].transform.position;   
    }

}
