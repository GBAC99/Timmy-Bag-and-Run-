using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Lista de todos los objetos del juego
    List<GameObject> sceneObjects = new List<GameObject>();

    //Lista de Objetos Spawneables
    public GameObject[] spawnableObjects;

    //Propiedades del control del juego
    public float gameSpeed;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnableObjects.Length; i++)
        {
            AddToList(spawnableObjects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(GameObject g)
    {
        sceneObjects.Add(g);
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
