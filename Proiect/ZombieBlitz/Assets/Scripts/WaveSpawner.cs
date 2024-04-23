using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum WaveSpawnerState
    {
        COUNTDOWN,
        SPAWNING,
        WAITING
    };

    public Wave[] waves;
    public Transform[] spawnPoints;
    int nextWave = 0;
    public float timeBetweenWaves;
    public float timeUntilNextWave;
    WaveSpawnerState state=WaveSpawnerState.COUNTDOWN;

    public GameObject allZombies;

    void Start()
    {
        timeUntilNextWave = 5;
    }

    void Update()
    {
        if(state == WaveSpawnerState.WAITING)
        {
            if(AreEnemiesAlive())
            {
                return;
            }
            else
            {
                WaveCompleted();
            }
        }
        if (timeUntilNextWave <= 0)
        {
            if(state != WaveSpawnerState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            timeUntilNextWave -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Starting new wave");
        state = WaveSpawnerState.SPAWNING;

        for(int i=0; i<wave.count; i++)
        {
            SpawnZombie(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = WaveSpawnerState.WAITING;
        yield break;

    }

    void WaveCompleted() {
        Debug.Log("Wave completed!");
        nextWave++;
        state = WaveSpawnerState.COUNTDOWN;
        timeUntilNextWave = timeBetweenWaves;
        if (nextWave > waves.Length - 1)
        {
            Debug.Log("Completed all waves! Starting over...");
            nextWave = 0;
        }
    }

    bool AreEnemiesAlive()
    {
        if(allZombies.GetComponent<ZombieCounter>().getZombiesLeft() > 0)
            return true;
        return false;
    }

    void SpawnZombie(Transform zombie)
    {
        Debug.Log("Spawing zombie...");
        Transform spawnpoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(zombie, spawnpoint.transform.position,spawnpoint.transform.rotation,allZombies.transform);
    }
}
