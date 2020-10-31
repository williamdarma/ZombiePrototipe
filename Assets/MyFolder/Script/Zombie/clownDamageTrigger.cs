using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clownDamageTrigger : MonoBehaviour
{
    public ClownZombieBehaviour czb;
    public bool Kepala;
    // Start is called before the first frame update
    void Start()
    {
        czb = GetComponentInParent<ClownZombieBehaviour>();
    }

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
            czb.TakingDamage(damage);
            other.gameObject.SetActive(false);
        }
    }
}
