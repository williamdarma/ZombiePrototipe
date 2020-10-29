using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClownZombieBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public ClownZombieClass CZC;
    public Transform Target;
    public Animator ZombieAnimator;
    public ZombieState CZS;
    public ParticleSystem BloodEffect;
    public GameObject ExplodeEffect;
    float exploderadius;
    float explotsionForce;
    float explosionDamagetoZombie;
    float explosionDamagetoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        CZC.clownZombieHP = 250;
        CZC.clownZombieSpeed = 4f;
        CZC.clownZombieAttackRange = .4f;
        agent.speed = CZC.clownZombieSpeed;
        CZS = ZombieState.Alive;
        exploderadius = 2;
        explosionDamagetoPlayer = 1;
        explosionDamagetoZombie = 125;
        explotsionForce = 1000;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        ZombieAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CZS == ZombieState.Alive)
        {
            float distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
            if (distance > CZC.clownZombieAttackRange *6)
            {
                agent.speed = CZC.clownZombieSpeed;
                agent.SetDestination(Target.position);
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
        if (distance < CZC.clownZombieAttackRange * 6)
        {
            print("PlyerTakingDamage");
            Target.GetComponent<PlayerBehaviour>().PlayerTakingDamage(.6f);
        }
    }
    public void TakingDamage(float damage)
    {
        print("ClownZombietakingDamage");
        if (CZS == ZombieState.Dead)
        {
            return;
        }
        CZC.clownZombieHP -= damage;
        BloodEffect.Play();
        if (CZC.clownZombieHP <= 0)
        {
            ZombieDeath();
        }

    }
    private void ZombieDeath()
    {
        CZS = ZombieState.Dead;
        StartCoroutine(ClownZombieSpecial());
    }


    IEnumerator ClownZombieSpecial()
    {
        float temp = 0;
        while (temp <=3)
        {
            agent.speed = CZC.clownZombieSpeed*2;
            agent.SetDestination(Target.position);
            temp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        explode();
    }

    void explode()
    {
        Instantiate(ExplodeEffect, transform.position, transform.rotation);

        Collider[] colls = Physics.OverlapSphere(transform.position, exploderadius);
        foreach(Collider near in colls)
        {
            Rigidbody rb = near.GetComponent<Rigidbody>();
            if (rb!= null)
            {
                rb.AddExplosionForce(explotsionForce, transform.position, exploderadius);
            }

            normalZombieBehaviour nzb = near.GetComponent<normalZombieBehaviour>();
            if (nzb != null)
            {
                nzb.TakingDamage(explosionDamagetoZombie);
            }
            PlayerBehaviour pb = near.GetComponent<PlayerBehaviour>();
            if (pb!= null)
            {
                pb.PlayerTakingDamage(explosionDamagetoPlayer);
            }

        }
        gameObject.SetActive(false);
    }

}
