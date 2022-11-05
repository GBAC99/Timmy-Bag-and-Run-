using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Movable
{

    Animator axeAnim;
    float animSpeed;

    public float swingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        axeAnim = gameObject.GetComponent<Animator>();
        swingSpeed = 1;
        animSpeed = axeAnim.GetCurrentAnimatorStateInfo(0).speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.actualState == "Play")
        {
            Move();
        }
        animSpeed = animSpeed * swingSpeed;
    }
}
