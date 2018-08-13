using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
    public class Wave
    {
        public string name;
        public string enemytag;
        public int count;
        public float rate;
    }

    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    public Wave[] waves;
    private int nextWave = 0;

    public float firstWaveWaitTime = 0f;
    public float timeBetweenWaves = 2f;
    public float waveCountDown;
    private SpawnState state = SpawnState.COUNTING;
    private float searchCountDown = 1f;

    ObjectPooler objectpooler;
    public float spawnPositionX;
    public float spawnPositionY;

    void Start () {
        waveCountDown = firstWaveWaitTime;
        objectpooler = ObjectPooler.instance;

    }
	
	void Update () {
        if (!GameManager.gameStarted) return;
        if (state == SpawnState.WAITING)
        {
            //Check Alive Enemy
            if (!EnemyIsAlive())
            {
                //Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

		if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
	}

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i< _wave.count; i++)
        {
            SpawnEnemy(_wave.enemytag);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(string _enemytag)
    {
        //Spawn Enemy
        int direction = 1;
        if (Random.value > 0.5f) direction = -1; 
        Vector2 spawnPosition = new Vector2(spawnPositionX * direction, Random.Range(-spawnPositionY, spawnPositionY+1));
        objectpooler.SpawnFromPool(_enemytag, spawnPosition, Quaternion.identity);
        Debug.Log("Spawning Enemy: " + _enemytag);
    }

    void WaveCompleted()
    {
        Debug.Log("A wave completed!");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = waves.Length - 1;
            Debug.Log("Finished Final Wave");
        }
        else
        {
            nextWave++;
        }
    }

}
