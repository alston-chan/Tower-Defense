using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int monsterCount;
    public Monster[] monsterType;
    public int[] typeAmount;
    public float spawnInterval;
    public static float time;
}



public class WaveSpawner : Singleton<WaveSpawner>
{

    public Wave[] waves;
    public Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    public int killsNeeded = 0;
    private int subWaveCount = 0; //the subwave that's spawning
    private int subWaveAmount = 0; //the amount of monsters per subwave
    private bool waveTrigger = true;

    public TMP_Text waveText;
    public Slider slide;

    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();

        waveText.text = "Wave: " + (currentWaveNumber + 1) + " / " + waves.Length;
        slide.value = 0;
        
    }


    public void Update()
    {
        currentWave = waves[currentWaveNumber];
        StartWave();
    }

    private void btnStartNextWave()
    {
        //FindGameObjectsWithTag efficiency can be improved. Maybe store the monster in the list when it spawns?
        //GameObject[] monstersSpawned = GameObject.FindGameObjectsWithTag("Monster");
        //monsterSpawned.Length == 0
        //To Do: remove monsterCount and just use the sum of typeAmounts or synchronize them somehow. Change spawnWave so it works without monsterCount. Clean up code
        if (killsNeeded == 0 && !canSpawn && currentWaveNumber != waves.Length)
        {
            waveTrigger = true;
            subWaveCount = 0;
            subWaveAmount = 0;
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        currentWaveNumber ++;
        canSpawn = true;
        subWaveAmount = currentWave.typeAmount[subWaveCount];


        waveText.text = "Wave: " + (currentWaveNumber + 1) + " / " + waves.Length;
        slide.value = (float) (currentWaveNumber + 1) / waves.Length;
    }

    private IEnumerator SpawnWave()
    {
        // for(var i = 0; i<typeAmount.length; i++)
        // {
        // killsNeeded += typeAmount[i];
        // }
        //Random.Range(0, currentWave.monsterType.Length)
        if (waveTrigger == true)
        {
            subWaveAmount = currentWave.typeAmount[subWaveCount];
            waveTrigger = false;
        }
        killsNeeded = currentWave.monsterCount;
        
        if (canSpawn && nextSpawnTime < Time.time)
        {
            Debug.Log("subWaveCount is" + subWaveCount);
            
            //if (subWaveAmount > 0)
            Monster monster = Pool.GetObject(currentWave.monsterType[subWaveCount].name).GetComponent<Monster>();
            monster.Spawn();
            currentWave.monsterCount --;
            subWaveAmount --;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.monsterCount == 0)
            {
                canSpawn = false;
            }
            if (subWaveAmount == 0)
            {  
                if ((subWaveCount + 1) < currentWave.typeAmount.Length)
                {
                    subWaveCount ++;
                }
                Debug.Log("subWaveCount is after change" + subWaveCount);
                subWaveAmount = currentWave.typeAmount[subWaveCount];
            }


            yield return new WaitForSeconds(2.5f);
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
        //SpawnWave();
    }
}
