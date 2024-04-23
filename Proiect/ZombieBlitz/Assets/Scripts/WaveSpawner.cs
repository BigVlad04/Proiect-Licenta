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
    int nextWave = 0;
    public float timeBetweenWaves;
    public float timeUntilNextWave;
    WaveSpawnerState state=WaveSpawnerState.COUNTDOWN;

    void Start()
    {
        timeUntilNextWave = 5;
    }

    void Update()
    {
        if(state == WaveSpawnerState.WAITING)
        {

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
        state = WaveSpawnerState.SPAWNING;

        for(int i=0; i<wave.count; i++)
        {
            SpawnZombie(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = WaveSpawnerState.WAITING;
        yield break;

    }

    bool AreEnemiesAlive()
    {
        return false;
    }

    void SpawnZombie(Transform zombie)
    {
        Debug.Log("Spawing zombie...");
    }
}
