using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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
    public int maxZombieinArea;
    public bool SedangSpecialWave;
    public bool BossWave;
    bool GameStart;


    [Header("UI")]
    public TextMeshProUGUI WaveStage;
    public TextMeshProUGUI EnemiesRemaining;

    public GameObject FinishGamePanel;

    public AudioSource ZombieMoan;

    // Start is called before the first frame update
    void Start()
    {
        SG = StageGame.Stage1;
        Wave = 0;
        WaveStage.text = "WAVE : " + 1;
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
        else if (SG == StageGame.Stage3)
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
        BossWave = true;
        while (BossWave)
        {
            if (TotalZombieinArea<15)
            {
                spawnNormalZombie(2);
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
            EnemiesRemaining.text = TotalZombieinArea.ToString();
    }

    public void ZombieDefeated()
    {
       if (!SedangSpecialWave)
       {
            TotalZombieinArea--;
            EnemiesRemaining.text = TotalZombieinArea.ToString();
            if (SG == StageGame.Stage3)
            {
                DefeatedZombie++;

                if (DefeatedZombie == 20)
                {
                    SpawnBoss();
                }
                if (TotalZombieinArea<= 0)
                {
                    finishGame(true);
                }
            }
            else
            {
                if (TotalZombieinArea <= 0)
                {
                    SpecialWave();
                }
            }  

       }
       else
       {
            TotalZombieinWave--;
            EnemiesRemaining.text = TotalZombieinWave.ToString();
            if (TotalZombieinWave <= 0)
            {
                SedangSpecialWave = false;
                Wave++;
                WaveStage.text = Wave + 1.ToString();
                if (Wave == 1)
                {
                    WaveStage.text = "WAVE : " + 2;
                    SG = StageGame.Stage2;
                }
                else if (Wave == 2)
                {
                    WaveStage.text = "FINAL WAVE";
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
        clown.SetActive(true);
        tempspawnPoint.RemoveAt(tempClown);
        TotalZombieinWave++;
        for (int i = 0; i < 2; i++)
        {
            int randomtemp = UnityEngine.Random.Range(0, tempspawnPoint.Count);
            for (int j = 0; j < 5; j++)
            {
                GameObject normalZ = Instantiate(NormalZ[UnityEngine.Random.Range(0, NormalZ.Length)]);
                normalZ.transform.position = tempspawnPoint[randomtemp].transform.position;
                normalZ.SetActive(true);
                TotalZombieinWave++;

            }
            tempspawnPoint.RemoveAt(randomtemp);
        }
        EnemiesRemaining.text = TotalZombieinWave.ToString();

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
        clown.SetActive(true);
        tempspawnPoint.RemoveAt(tempClown);
        TotalZombieinWave++;

        int randomtemp = UnityEngine.Random.Range(0, tempspawnPoint.Count);
        for (int j = 0; j < 10; j++)
        {
            GameObject normalZ = Instantiate(NormalZ[UnityEngine.Random.Range(0, NormalZ.Length)]);
            normalZ.transform.position = tempspawnPoint[randomtemp].transform.position;
            normalZ.GetComponent<normalZombieBehaviour>().BoostStats(50);
            normalZ.SetActive(true);
            TotalZombieinWave++;

        }
        tempspawnPoint.RemoveAt(randomtemp);

        int randomtemp1 = UnityEngine.Random.Range(0, tempspawnPoint.Count);
        for (int j = 0; j < 5; j++)
        {
            GameObject normalZ = Instantiate(NormalZ[UnityEngine.Random.Range(0, NormalZ.Length)]);
            normalZ.transform.position = tempspawnPoint[randomtemp1].transform.position;
            normalZ.GetComponent<normalZombieBehaviour>().BoostStats(50);
            normalZ.SetActive(true);
            TotalZombieinWave++;
        }

        GameObject clown1 = Instantiate(ClownZ);
        clown1.transform.position = tempspawnPoint[randomtemp1].transform.position;
        clown1.SetActive(true);
        TotalZombieinWave++;
        EnemiesRemaining.text = TotalZombieinWave.ToString();
        tempspawnPoint.RemoveAt(randomtemp1);
    }

    void SpawnBoss()
    {
        GameObject bossZombie = Instantiate(BossZ);
        bossZombie.transform.position = BossSpawnPoint.transform.position;
        bossZombie.SetActive(true);
    }

    public void BossZombiedefeated()
    {
        BossWave = false;
    }

    public void finishGame(bool Win)
    {
        if (Win)
        {
            FinishGamePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Congratulation";
            FinishGamePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "you Survived";

        }
        else
        {
            FinishGamePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "GAME OVER";
            FinishGamePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "YOU'RE DEAD";
        }
        FinishGamePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (TotalZombieinArea > 0 || TotalZombieinWave > 0)
        {
            ZombieMoan.gameObject.SetActive(true);
        }
        else
        {
            ZombieMoan.gameObject.SetActive(false);
        }
    }
}






