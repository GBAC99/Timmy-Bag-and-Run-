using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Movable
{

    public Chain()
    {

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
}
