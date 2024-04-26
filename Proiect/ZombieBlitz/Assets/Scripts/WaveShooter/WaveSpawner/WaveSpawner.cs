using System.Collections;
using UnityEngine;
/// <summary>
/// this script makes zombie waves appear at set time intervals.
/// </summary>
public class WaveSpawner : MonoBehaviour
{

    //maybe make endless gamemode where waves are generated automatically forever
    public enum WaveSpawnerState
    {
        COUNTDOWN,      //counting down until the start of the next wave
        SPAWNING,       //spawning enemies
        WAITING         //waiting for the end of the current wave
    };
    //maybe make the script spawn zombies without waiting for the player to kill off current zombies.
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject allZombies;   //used for counting the number of remaining zombies and is also a parent object to all the zombies that will spawn
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
            if(AreEnemiesAlive())
            {
                return;     //wait for the player to kill all zombies before starting next wave
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
        Debug.Log("Starting wave " + (nextWave+1));
        state = WaveSpawnerState.SPAWNING;
        for(int i=0; i<wave.zombieTypes.Length;i++)      //for each zombie type
        {
            for (int j = 0; j < wave.numberOfZombies[i]; j++)       //spawn the corresponding zombie 
            {
                SpawnZombie(wave.zombieTypes[i]);
                yield return new WaitForSeconds(1f / wave.spawnRate);
            }
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
            Debug.Log("Completed all waves! Starting over...");     //maybe add winning screen
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

    public int getWaveNumber()
    {
        return nextWave +1;
    }
}
