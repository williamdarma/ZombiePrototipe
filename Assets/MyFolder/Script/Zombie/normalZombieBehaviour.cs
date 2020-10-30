using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState { Alive, Eating, Dead };
public class normalZombieBehaviour : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform Target;
    public bool alive;
    public Animator ZombieAnimator;
    public ZombieState ZS;
    public ParticleSystem BloodEffect;
    public GameLevelManager GLM;

    [Header("ZombieStats")]
    float normalZombieHP;
    float normalZombieSpeed ;
    float normalZombieAttackRange;
    float normalZombieDamage;

    private void OnEnable()
    {
        initialzeStats();
    }

    void initialzeStats()
    {
        normalZombieHP = 100 + (UnityEngine.Random.Range(0, 4) * 25);
        normalZombieSpeed = 3;
        normalZombieAttackRange = .4f;
        normalZombieDamage = .6f;
        agent.speed = normalZombieSpeed;
        ZS = ZombieState.Alive;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        GLM = GameObject.FindObjectOfType<GameLevelManager>();
        ZombieAnimator = GetComponent<Animator>();
    }

    public void BoostStats(float boost)
    {
        normalZombieHP += boost;
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
            if (distance> normalZombieAttackRange*6 )
            {
                agent.speed = normalZombieSpeed;
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
        if (distance < normalZombieAttackRange*6)
        {
            print("PlyerTakingDamage");
            Target.GetComponent<PlayerBehaviour>().PlayerTakingDamage(normalZombieDamage);
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
        normalZombieHP -= damage;

        if (normalZombieHP <=0)
        {
            normalZombieHP = 0;
            ZombieDeath();
        }
    }

    private void ZombieDeath()
    {
        ZS = ZombieState.Dead;
        GLM.ZombieDefeated();
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakingDamage(40);
            other.gameObject.SetActive(false);
        }
    }
}
