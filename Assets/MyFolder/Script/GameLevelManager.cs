using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{

    public GameObject[] NormalZ;
    public GameObject ClownZ, BossZ, Player;
    public Transform[] ZombieSpawnPoint;
    public Transform PlayerSpawnPoint;
    public Transform BossSpawnPoint;
    public int jumlahpooling;
    public List<GameObject> listNormalZombie;

    [Header("LevelStage")]
    public int Wave;
    public int DefeatedZombie;
    public int TotalZombieinArea;
    int maxZombieinArea;
    // Start is called before the first frame update
    void Start()
    {
        initializeGame();
    }

    void initializeGame()
    {
        Player.transform.position = PlayerSpawnPoint.position;
        maxZombieinArea = 10;
    }

    IEnumerator initializeWave1()
    {
        yield return new WaitForSeconds(5f);
        int temp = 0;
        while(temp < maxZombieinArea)
        {
            spawnNormalZombie(0);
            temp++;
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator initializeWave2()
    {    
        int temp = 0;
        while (temp < maxZombieinArea)
        {
            spawnNormalZombie(1);
            temp++;
            yield return new WaitForSeconds(2f);
        }
    }

    void spawnNormalZombie(int lvl)
    {
        List<Transform> tempSpawnPoint = new List<Transform>();
        for (int i = 0; i < ZombieSpawnPoint.Length; i++)
        {
            float distance = Vector3.Distance(ZombieSpawnPoint[i].position, Player.transform.position);
            if (distance > 5)
            {
                tempSpawnPoint.Add(ZombieSpawnPoint[i].transform);
            }
        }
        int selectedZombie = UnityEngine.Random.Range(0, NormalZ.Length);
        int selectedSpawnPoint = UnityEngine.Random.Range(0, tempSpawnPoint.Count);

        GameObject temp = Instantiate(NormalZ[selectedZombie]);
        temp.transform.position = tempSpawnPoint[selectedSpawnPoint].position;
        temp.SetActive(true);
        if (lvl == 1)
        {
            temp.GetComponent<normalZombieBehaviour>().BoostStats(50);
        }
        else if (lvl ==2)
        {
            temp.GetComponent<normalZombieBehaviour>().BoostStats(100);
        }
        TotalZombieinArea++;
    }

    public void ZombieDefeated()
    {
        TotalZombieinArea--;
        DefeatedZombie++;
        if (TotalZombieinArea<=0)
        {
            Specialwave(Wave);
        }
    }

    private void Specialwave(int wave)
    {
        throw new NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
