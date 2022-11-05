using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    //Referencia al Game Manager
    GameManager gameManager;

    //Estadisticas de los objetos 
    public float speed;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddToList(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
    }

    void Move() //Movimiento del objeto
    {
        transform.Translate(Vector3.right * speed * gameManager.gameSpeed * Time.deltaTime);
    }
}
