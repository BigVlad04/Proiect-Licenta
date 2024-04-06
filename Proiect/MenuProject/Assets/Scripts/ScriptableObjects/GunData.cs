using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector]
    public bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
