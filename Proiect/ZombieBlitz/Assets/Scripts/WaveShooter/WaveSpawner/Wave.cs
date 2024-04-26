using UnityEngine;
/// <summary>
/// this class holds information about a wave
/// </summary>
[System.Serializable]
public class Wave 
{
    public string waveName;         
    public Transform[] zombieTypes;  
    public int[] numberOfZombies;
    public float spawnRate;
}