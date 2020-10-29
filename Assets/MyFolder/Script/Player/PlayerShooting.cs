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
    // Start is called before the first frame update
    void Start()
    {
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
            print(peluru.transform.position);
            print(peluru.name);
            jumlahpelet--;
        }
    }
    public void Reload()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
