using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    //Referencia al Game Manager
    protected GameManager gameManager;

    //Propiedades de los objetos 
    public float speed;
    Vector3 initialPosition;
    GameObject _thisGO;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddToList(gameObject);
        initialPosition = this.transform.position;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move() //Movimiento del objeto
    {
        transform.Translate(Vector3.right * speed * gameManager.actualGameSpeed * Time.deltaTime);
    }

    public void Restart()
    {
        transform.position = initialPosition;
    }

}
