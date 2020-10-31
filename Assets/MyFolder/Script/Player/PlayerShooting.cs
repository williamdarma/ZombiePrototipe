using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject playerBullet;
    public GameObject bulletSpawnPoint;
    public List<GameObject> listBullet;
    public int jumlahPooling;
    public int jumlahpelet = 24;
    PlayerBehaviour pb;
    PlayerMovementScript pms;
    public AudioSource reload;
    public AudioSource shoot;
    // Start is called before the first frame update
    void Start()
    {
        pb = GetComponent<PlayerBehaviour>();
        pms = GetComponent<PlayerMovementScript>();
        for (int i = 0; i < jumlahPooling; i++)
        {
            GameObject temp = Instantiate(playerBullet);
            temp.SetActive(false);
            listBullet.Add(temp);
        }
    }


    GameObject Pelet ()
    {
        for (int i = 0; i< listBullet.Count; i++)
        {
            if (!listBullet[i].activeInHierarchy)
            {
                return listBullet[i];
            }
        }
        return null;
    }
    public void Shoot()
    {
        GameObject peluru = Pelet();
        if (peluru!= null)
        {
            peluru.transform.position = bulletSpawnPoint.transform.position;
            peluru.transform.rotation = bulletSpawnPoint.transform.rotation;
            peluru.SetActive(true);
            shoot.Play();
            jumlahpelet--;
            pb.ChangeuiBullet(jumlahpelet);
        }
    }
    public void Reload()
    {
        jumlahpelet = 24;
        pb.ChangeuiBullet(jumlahpelet);
        pms.PM = PlayerMovement.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
