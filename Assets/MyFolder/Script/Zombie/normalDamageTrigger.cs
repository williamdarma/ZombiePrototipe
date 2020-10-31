using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalDamageTrigger : MonoBehaviour
{
    public normalZombieBehaviour nzb;
    public bool Kepala;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            print("kena pelor");
            float damage = other.GetComponent<BulletMovment>().bulletDamage;
            if (Kepala)
            {
                damage = damage * 2;
            }
            nzb.TakingDamage(damage);
            other.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        nzb = GetComponentInParent<normalZombieBehaviour>();
    }
}
