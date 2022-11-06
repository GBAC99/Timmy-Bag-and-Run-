using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Movable
{
    public enum PotionType
    {
        STAMINA,
        INMUNE,
        SLOW


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float staminaPoints;
    public float slowTime;
    public float slowGameSpeed;
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
                TakeStamina();
                break;
            case PotionType.INMUNE:
                TakeInmune();
                break;
            case PotionType.SLOW:
                TakeSlow();
                break;
            default:
                break;
        }
        gameObject.SetActive(false);

    }

    void TakeStamina()
    {
        gameManager.player.FillStamina(staminaPoints);
    }
    void TakeInmune()
    {
        gameManager.player.SetPlayerInvencible();
    }
    void TakeSlow()
    {
        gameManager.SlowGame(slowTime,slowGameSpeed);
    }

}
