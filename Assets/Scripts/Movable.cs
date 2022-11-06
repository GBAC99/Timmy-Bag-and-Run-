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
    public MeshRenderer[] meshRenderer;

    public Movable()
    {
        speed = 1;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddToList(gameObject);
        initialPosition = this.transform.position;

        foreach (MeshRenderer m in meshRenderer)
        {
            m.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.actualState == "Play")
        {
            Move();
        }
    }

    public void Move() //Movimiento del objeto
    {
        transform.Translate(Vector3.right * speed * gameManager.actualGameSpeed * Time.deltaTime);
    }

    public void Restart()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        foreach (MeshRenderer m in meshRenderer)
        {
            m.enabled = false;
        }
        transform.position = initialPosition;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Renderer")
        {
            foreach (MeshRenderer m in meshRenderer)
            {
                if (!m.enabled)
                {
                    m.enabled = true;
                }
            }
        }
        if (other.gameObject.tag == "Destroyer")
        {
            gameObject.SetActive(false);
        }
    }

}
