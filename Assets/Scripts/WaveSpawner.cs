using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int monsterCount;
    public Monster[] monsterType;
    public float spawnInterval;
    public static float time;
}



public class WaveSpawner : Singleton<WaveSpawner>
{

    public Wave[] waves;
    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;

    public TMP_Text waveText;

    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();

        waveText.text = "Wave: " + (currentWaveNumber + 1) + " / " + waves.Length;
    }


    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        StartWave();
    }

    public void btnStartNextWave()
    {
        //FindGameObjectsWithTag efficiency can be improved. Maybe store the monster in the list when it spawns?
        GameObject[] monstersSpawned = GameObject.FindGameObjectsWithTag("Monster");
        if (monstersSpawned.Length == 0 && !canSpawn && currentWaveNumber != waves.Length)
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        currentWaveNumber ++;
        canSpawn = true;

        waveText.text = "Wave: " + (currentWaveNumber + 1) + " / " + waves.Length;
    }

    private IEnumerator SpawnWave()
    {

        if (canSpawn && nextSpawnTime < Time.time)
        {
            Monster monster = Pool.GetObject(currentWave.monsterType[Random.Range(0, currentWave.monsterType.Length)].name).GetComponent<Monster>();
            monster.Spawn();
            currentWave.monsterCount --;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.monsterCount == 0)
            {
                canSpawn = false;
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
