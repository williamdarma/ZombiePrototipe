using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerClass PC;
    public bool Alive;
    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
    }

    public void PlayerTakingDamage(float damage)
    {
        if (Alive)
        {
            PC.PlayerLives -= damage;
            if (PC.PlayerLives <= 0)
            {
                PC.PlayerLives = 0;
                Alive = false;
                
            }
        }
    }

}
