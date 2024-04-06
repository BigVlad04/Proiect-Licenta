using UnityEngine;
/// <summary>
/// Will store information about each weapon in the game.
/// </summary>
[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float range;
    public int magazineSize;
    public int currentAmmo;
    public float fireRate;
    public float reloadTime;
    public bool isAutomatic;
    public AudioClip shootingSound;
    public AudioClip reloadSound;
    public AudioClip dryFireSound;
    public float dryFireDuration;
    [HideInInspector]
    public bool reloading;
}
