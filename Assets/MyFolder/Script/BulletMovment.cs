using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : MonoBehaviour
{

    public float bulletSpeed;
    public float bulletDamage;
    Rigidbody bulletRB;
    private void OnEnable()
    {
        Invoke("destroyBullet", 2);
    }


    void destroyBullet()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Props"))
        {
            gameObject.SetActive(false);
        }
    }
}
