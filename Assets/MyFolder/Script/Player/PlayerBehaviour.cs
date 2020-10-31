using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public float playerHealth, playerSpeed, maxHealth;
    public bool Alive;
    PlayerShooting PS;
    GameLevelManager glm;

    [Header("UI")]
    public Image PlayerHealthBar;
    public TextMeshProUGUI bulletMags, Reload;
    public Color Full, Medium, Critical;

    public AudioSource takingdamage;
    // Start is called before the first frame update
    void Start()
    {
        glm = GameObject.FindObjectOfType<GameLevelManager>();
        Alive = true;
        maxHealth = playerHealth;
        StartCoroutine(playerRecovery());
    }

    public void PlayerTakingDamage(float damage)
    {
        if (Alive)
        {
            Handheld.Vibrate();
            takingdamage.Play();
            playerHealth -= damage;
            ChangeSlider(playerHealth);
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                Alive = false;
                glm.finishGame(false);
            }
        }
    }

    public void ChangeSlider(float health)
    {
        
        PlayerHealthBar.fillAmount = health / maxHealth;
        if(PlayerHealthBar.fillAmount>.5f)
        {
            PlayerHealthBar.color = Full;
        }
        else if (PlayerHealthBar.fillAmount > .2f)
        {
            PlayerHealthBar.color = Medium;
        }
        else
        {
            PlayerHealthBar.color = Critical;
        }
    }

    public void ChangeuiBullet(float ammount)
    {
        bulletMags.text = "x" + ammount;
    }

    IEnumerator playerRecovery()
    {
        while (Alive)
        {
            yield return new WaitForSeconds(4f);
            playerHealth += .08f;
            if (playerHealth>= maxHealth)
            {
                playerHealth = maxHealth;
            }
            ChangeSlider(playerHealth);
        }
    }

}
