using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StageGame {Stage1, Stage2, Stage3};
public class GameLevelManager : MonoBehaviour
{

    public GameObject[] NormalZ;
    public GameObject ClownZ, BossZ, Player;
    public Transform[] ZombieSpawnPoint;
    public Transform PlayerSpawnPoint;
    public Transform BossSpawnPoint;
    public int jumlahpooling;
    public List<GameObject> listNormalZombie;
    public StageGame SG;

    [Header("LevelStage")]
    public int Wave;
    public int DefeatedZombie;
    public int TotalZombieinArea;
    public int TotalZombieinWave;
    int maxZombieinArea;
    bool SedangSpecialWave;
    bool GameStart;
    // Start is called before the first frame update
    void Start()
    {
        SG = StageGame.Stage1;
        Wave = 0;
        initializeGame();
    }

    void initializeGame()
    {
        Player.transform.position = PlayerSpawnPoint.position;
        if (SG == StageGame.Stage1)
        {
            maxZombieinArea = 10;
            StartCoroutine(initializeWave1());
        }
        else if (SG == StageGame.Stage2)
        {
            maxZombieinArea = 15;
            StartCoroutine(initializeWave2());
        }
        else if (SG == StageGame.Stage2)
        {
            maxZombieinArea = 20;
            StartCoroutine(initializeWave3());
        }
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
        yield return new WaitForSeconds(3f);
        int temp = 0;
        while (temp < maxZombieinArea)
        {
            if (TotalZombieinArea <10)
            {
                spawnNormalZombie(1);
                temp++;
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return null;
            }
           
        }
    }

    IEnumerator initializeWave3()
    {
        yield return new WaitForSeconds(1f);
        int temp = 0;
        while (temp < maxZombieinArea)
        {
            if (TotalZombieinArea<15)
            {
                spawnNormalZombie(2);
                temp++;
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return null;
            }

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

       if (!SedangSpecialWave)
       {
            TotalZombieinArea--;
            if (SG == StageGame.Stage3)
            {
                DefeatedZombie++;

                if (DefeatedZombie == 20)
                {

                    SpawnBoss();
                }
            }
            if (TotalZombieinArea<=0)
            {
                if (SG != StageGame.Stage3)
                {
                    SpecialWave();
                }

            }

       }
       else
       {
            TotalZombieinWave--;
            if (TotalZombieinWave <= 0)
            {
                SedangSpecialWave = false;
                Wave++;
                if (Wave == 1)
                {
                    SG = StageGame.Stage2;
                }
                else if (Wave == 2)
                {
                    SG = StageGame.Stage3;
                }
                initializeGame();
            }
        }

    }

    private void SpecialWave()
    {
        SedangSpecialWave = true;
        if (SG == StageGame.Stage1)
        {
            SpawnSpecialWave1();
        }
        else if (SG == StageGame.Stage2)
        {
            SpawnSpecialWave2();
        }
        else if (SG == StageGame.Stage3)
        {

        }
    }

    private void SpawnSpecialWave1()
    {
        List<Transform> tempspawnPoint = new List<Transform>();
        for (int i = 0; i < ZombieSpawnPoint.Length; i++)
        {
            tempspawnPoint.Add(ZombieSpawnPoint[i]);
        }

        GameObject clown = Instantiate(ClownZ);
        int tempClown = UnityEngine.Random.Range(0, tempspawnPoint.Count);
        clown.transform.position = tempspawnPoint[tempClown].transform.position;
        tempspawnPoint.RemoveAt(tempClown);
        TotalZombieinWave++;
        for (int i = 0; i < 2; i++)
        {
            int randomtemp = UnityEngine.Random.Range(0, tempspawnPoint.Count);
            for (int j = 0; j < 5; j++)
            {
                GameObject normalZ = Instantiate(NormalZ[UnityEngine.Random.Range(0, NormalZ.Length)]);
                normalZ.transform.position = tempspawnPoint[randomtemp].transform.position;
                TotalZombieinWave++;

            }
            tempspawnPoint.RemoveAt(randomtemp);
        }

    }

    private void SpawnSpecialWave2()
    {
        List<Transform> tempspawnPoint = new List<Transform>();
        for (int i = 0; i < ZombieSpawnPoint.Length; i++)
        {
            tempspawnPoint.Add(ZombieSpawnPoint[i]);
        }
        GameObject clown = Instantiate(ClownZ);
        int tempClown = UnityEngine.Random.Range(0, tempspawnPoint.Count);
        clown.transform.position = tempspawnPoint[tempClown].transform.position;
        tempspawnPoint.RemoveAt(tempClown);
        TotalZombieinWave++;

        int randomtemp = UnityEngine.Random.Range(0, tempspawnPoint.Count);
        for (int j = 0; j < 10; j++)
        {
            GameObject normalZ = Instantiate(NormalZ[UnityEngine.Random.Range(0, NormalZ.Length)]);
            normalZ.transform.position = tempspawnPoint[randomtemp].transform.position;
            normalZ.GetComponent<normalZombieBehaviour>().BoostStats(50);
            TotalZombieinWave++;

        }
        tempspawnPoint.RemoveAt(randomtemp);

        int randomtemp1 = UnityEngine.Random.Range(0, tempspawnPoint.Count);
        for (int j = 0; j < 5; j++)
        {
            GameObject normalZ = Instantiate(NormalZ[UnityEngine.Random.Range(0, NormalZ.Length)]);
            normalZ.transform.position = tempspawnPoint[randomtemp1].transform.position;
            normalZ.GetComponent<normalZombieBehaviour>().BoostStats(50);
            TotalZombieinWave++;
        }

        GameObject clown1 = Instantiate(ClownZ);
        clown1.transform.position = tempspawnPoint[randomtemp1].transform.position;
        TotalZombieinWave++;
        tempspawnPoint.RemoveAt(randomtemp1);
    }

    void SpawnBoss()
    {
        GameObject bossZombie = Instantiate(BossZ);
        bossZombie.transform.position = BossSpawnPoint.transform.position;
    }



    // Update is called once per frame
    void Update()
    {

    }
}






