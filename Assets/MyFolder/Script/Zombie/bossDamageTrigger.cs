using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDamageTrigger : MonoBehaviour
{
    public BossZombieBehaviour bzb;
    public bool Kepala;
    // Start is called before the first frame update
    void Start()
    {
        bzb = GetComponentInParent<BossZombieBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            float damage = other.GetComponent<BulletMovment>().bulletDamage;
            if (Kepala)
            {
                damage = damage * 2;
            }
            bzb.TakingDamage(damage);
        }
    }
}
