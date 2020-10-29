using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombieBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Target;
    public Animator ZombieAnimator;
    public ZombieState BZS;
    bool stateTransition;
    [Header("ZombieStats")]
    float BossZombieHP;
    float BossZombieSpeed;
    float BossZombieAttackRange;
    float BossZombieScale;
    float BossZombieDamage;
    int BossZombieState;
    

    private void OnEnable()
    {
        initializeStats(0);
    }

    private void initializeStats(int State)
    {
        if (BossZombieState == 0)
        {
            BossZombieHP = 2500;
            BossZombieSpeed = 11;
            BossZombieAttackRange = 1.5f;
            BossZombieScale = 2.5f;
            BossZombieDamage = 2f;
        }
        else if(BossZombieState == 1)
        {
            BossZombieHP = 1600;
            BossZombieSpeed = 8;
            BossZombieAttackRange = 1f;
            BossZombieScale = 1.8f;
            BossZombieDamage = 1.3f;
        }
        else if (BossZombieState == 1)
        {
            BossZombieHP = 1600;
            BossZombieSpeed = 5;
            BossZombieAttackRange = .6f;
            BossZombieScale = 1f;
            BossZombieDamage = .85f;
        }
        agent.speed = BossZombieSpeed;
        gameObject.transform.localScale = new Vector3(BossZombieScale, BossZombieScale, BossZombieScale);
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        ZombieAnimator = GetComponent<Animator>();
        BZS = ZombieState.Alive;
        stateTransition = false;
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
        if (BossZombieState < 2)
        {
            stateTransition = true;
            yield return new WaitForSeconds(2f);
            initializeStats(BossZombieState + 1);
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
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakingDamage(40);
        }

    }
}
