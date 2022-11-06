using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Movable
{
    public enum PotionType
    {
        STAMINA,
        INMUNE,
        STOP


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float staminaPoints;
    public float inmuneTime;
    public float stopTime;
    public PotionType pType;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.actualState == "Play")
        {
            Move();
        }
    }

    public void TakePickUp()
    {
        switch (pType)
        {
            case PotionType.STAMINA:

                break;
            case PotionType.INMUNE:
                break;
            case PotionType.STOP:
                break;
            default:
                break;
        }
    }

    void TakeStamina()
    {
        gameManager.player.FillStamina(staminaPoints);
    }
    void TakeInmune()
    {

    }
    void TakeStop()
    {

    }

}
