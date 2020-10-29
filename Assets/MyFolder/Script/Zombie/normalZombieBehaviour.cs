using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState { Alive, Eating, Dead };
public class normalZombieBehaviour : MonoBehaviour
{

    public NavMeshAgent agent;
    public NormalZombieClass NZC;
    public Transform Target;
    public bool alive;
    public Animator ZombieAnimator;
    public ZombieState ZS;
    public ParticleSystem BloodEffect;


    private void OnEnable()
    {
        NZC.normalZombieHP = 100 + (UnityEngine.Random.Range(0, 4) * 25);
        NZC.normalZombieSpeed = 3;
        NZC.normalZombieAttackRange = .4f;
        agent.speed = NZC.normalZombieSpeed;
        ZS = ZombieState.Alive;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        ZombieAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ZS == ZombieState.Alive)
        {
            float distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
            if (distance> NZC.normalZombieAttackRange*6 )
            {
                agent.speed = NZC.normalZombieSpeed;
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

    public void ZombieEat()
    {
        float distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        if (distance < NZC.normalZombieAttackRange*6)
        {
            print("PlyerTakingDamage");
            Target.GetComponent<PlayerBehaviour>().PlayerTakingDamage(.6f);
        }
    }

    public void TakingDamage(float damage)
    {
        print("normalZombietakingDamage");
        if (ZS == ZombieState.Dead)
        {
            return;
        }
        BloodEffect.Play();
        NZC.normalZombieHP -= damage;

        if (NZC.normalZombieHP <=0)
        {
            ZombieDeath();
        }
    }

    private void ZombieDeath()
    {
        ZS = ZombieState.Dead;
        gameObject.SetActive(false);
    }
}
