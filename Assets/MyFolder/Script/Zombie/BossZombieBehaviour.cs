﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class BossZombieBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Target;
    public Animator ZombieAnimator;
    public ZombieState BZS;
    bool stateTransition;
    [Header("ZombieStats")]
    float BossZombieHP;
    float bossmaxHP;
    float BossZombieSpeed;
    float BossZombieAttackRange;
    float BossZombieScale;
    float BossZombieDamage;
    int BossZombieStage;

    [Header("UI")]
    public Image bossHPBar;
    public TextMeshProUGUI bossStage;
    

    private void OnEnable()
    {
        BossZombieStage = 0;
        initializeStats(BossZombieStage);
    }

    private void initializeStats(int State)
    {
        if (BossZombieStage == 0)
        {
            BossZombieHP = 2500;
            BossZombieSpeed = 11;
            BossZombieAttackRange = 1.5f;
            BossZombieScale = 2.5f;
            BossZombieDamage = 2f;
        }
        else if(BossZombieStage == 1)
        {
            BossZombieHP = 1600;
            BossZombieSpeed = 8;
            BossZombieAttackRange = 1f;
            BossZombieScale = 1.8f;
            BossZombieDamage = 1.3f;
        }
        else if (BossZombieStage == 2)
        {
            BossZombieHP = 1600;
            BossZombieSpeed = 5;
            BossZombieAttackRange = .6f;
            BossZombieScale = 1f;
            BossZombieDamage = .85f;
        }
        bossmaxHP = BossZombieHP;
        agent.speed = BossZombieSpeed;
        gameObject.transform.localScale = new Vector3(BossZombieScale, BossZombieScale, BossZombieScale);
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        ZombieAnimator = GetComponent<Animator>();
        BZS = ZombieState.Alive;
        stateTransition = false;
        ChangeSliderHPBar(BossZombieHP);
    }


    void ChangeSliderHPBar(float amount)
    {
        bossHPBar.fillAmount = amount / bossmaxHP;
    }

    public void ZombieEat()
    {
        float distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        if (distance < BossZombieAttackRange)
        {
            print("PlyerTakingDamage");
            Target.GetComponent<PlayerBehaviour>().PlayerTakingDamage(BossZombieDamage);
        }
    }
    public void TakingDamage(float damage)
    {
        if (BZS == ZombieState.Dead)
        {
            return;
        }
        if (stateTransition)
        {
            return;
        }
        BossZombieHP -= damage;
        ChangeSliderHPBar(BossZombieHP);
        if (BossZombieHP<= 0)
        {
            BossZombieHP = 0;
            checkifBossDied();
        }

    }

    private void checkifBossDied()
    {
        StartCoroutine(iecheckifBossDied());
    }

    IEnumerator iecheckifBossDied()
    {
        if (BossZombieStage < 2)
        {
            stateTransition = true;
            yield return new WaitForSeconds(2f);
            initializeStats(BossZombieStage + 1);
        }
        else
        {
            BossZombieDied();
        }
    }

    private void BossZombieDied()
    {
        BZS = ZombieState.Dead;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BZS == ZombieState.Alive)
        {
            float distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
            if (distance > BossZombieAttackRange * 6)
            {
                agent.speed = BossZombieSpeed;
                agent.SetDestination(Target.position);
                // ZombieAnimator.SetInteger()
                ZombieAnimator.SetInteger("ZombieActivity", 1);
            }
            else
            {
                agent.speed = 0;
                ZombieAnimator.SetInteger("ZombieActivity", 2);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakingDamage(40);
        }

    }
}
