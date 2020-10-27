using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class normalZombieBehaviour : MonoBehaviour
{

    public NavMeshAgent agent;
    public NormalZombieClass NZC;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Target.position);
    }
}
