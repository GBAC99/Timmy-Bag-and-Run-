using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Movable
{

    public SkinnedMeshRenderer skinedmesh;



    // Start is called before the first frame update
    void Start()
    {
        skinedmesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.actualState == "Play")
        {
            Move();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Renderer")
        {
            skinedmesh.enabled = true;
            meshRenderer[0].enabled = true;
        }
    }

}
