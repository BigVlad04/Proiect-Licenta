using System.Collections;
using UnityEngine;
/// <summary>
/// This script handles gun mechanics (firing, reloading, effects)
/// </summary>
public class GunScript : MonoBehaviour
{
    //general
    [SerializeField] GunData gunData;   //will store information about the weapon
    public Camera playerCam;
    public Animator gunAnimator;
    public GameObject muzzleEffect;
    public GameObject impactEffectEnvironment;
    public GameObject impactEffectZombie;
    public AudioSource audioSource;

    //fire-time related
    private float nextFireTime = 0f;    //when the weapon can fire next
    private float nextDryFireTime = 0f; //when the weapon is out of ammo, a dry fire sound is played, and the time between sounds is calculated based on dryFireDuration

    private void Start()
    {
        audioSource =transform.parent.GetComponent<AudioSource>();      //The audio source will be the same for each weapon, namely the AudioSource of WeaponHolder
        gunAnimator = GetComponent<Animator>();
        gunData.currentAmmo = gunData.magazineSize;
    }

    private void OnEnable()
    {
        gunData.reloading = false;
        gunAnimator.SetBool("RELOADING", false);
    }

    void Update()
    {
        if (gunData.reloading)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R)) 
            //(gunData.currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)))        --> this option makes reloading automatic   
        {
            audioSource.PlayOneShot(gunData.reloadSound);
            StartCoroutine(ReloadWeapon());
            return;
        }
        if (Input.GetButton("Fire1"))
        {
            if (gunData.currentAmmo > 0 && Time.time >= nextFireTime )  
            {
                nextFireTime = Time.time + 1f / gunData.fireRate;
                Shoot();
            }
            else if (gunData.currentAmmo == 0 && Time.time >= nextDryFireTime) {    //dry fire
                nextDryFireTime = Time.time + gunData.dryFireDuration;
                audioSource.PlayOneShot(gunData.dryFireSound);
            }
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(gunData.shootingSound);     //fire sound
        gunAnimator.SetTrigger("RECOIL");                   //add recoil
        muzzleEffect.GetComponent<ParticleSystem>().Play(); //add muzzle flash
        gunData.currentAmmo--;
        HandleShot();
        
    }

    void HandleShot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, gunData.range))
        {

            ZombieController targetScript = hit.transform.GetComponentInParent<ZombieController>();
            if (targetScript != null)
            {
                if (hit.transform.gameObject.tag.Equals("Head"))
                {
                    targetScript.TakeDamage(gunData.damage * 2f);
                    Debug.Log(Time.time + " " + hit.transform.name + " " + gunData.damage * 1.5f);     //for debugging purposes
                }
                else if (hit.transform.gameObject.tag.Equals("Body"))
                {
                    Debug.Log(Time.time + " " + hit.transform.name + " " + gunData.damage);     //for debugging purposes
                    targetScript.TakeDamage(gunData.damage);    //if hit target, apply damage
                }
                else if (hit.transform.gameObject.tag.Equals("Legs"))
                {
                    Debug.Log(Time.time + " " + hit.transform.name + " " + gunData.damage*0.66f);     //for debugging purposes
                    targetScript.TakeDamage(gunData.damage * 0.66f);
                }
                else if (hit.transform.gameObject.tag.Equals("Arms"))
                {
                    Debug.Log(Time.time + " " + hit.transform.name + " " + gunData.damage * 0.75f);     //for debugging purposes
                    targetScript.TakeDamage(gunData.damage * 0.66f);
                }

            }
            HandleImpactEffects(hit);

            /*   if (hit.rigidbody != null) {
                   hit.rigidbody.AddForce(-hit.normal * gunData.damage);           ---> OPTIONAL: add impact force
               }*/
        }
    }

    void HandleImpactEffects(RaycastHit hit) {
        if (hit.transform.gameObject.layer != 6)
        {
            if(hit.transform.gameObject.layer == 7)         //impact effect for zombie
            {
                GameObject impactPoint = Instantiate(impactEffectZombie, hit.point, Quaternion.LookRotation(hit.normal)); //add bullet hole
                Destroy(impactPoint, 1f);      //destroy bullet hole

            }
            else            //impact effect for environment
            {
                GameObject impactPoint = Instantiate(impactEffectEnvironment, hit.point, Quaternion.LookRotation(hit.normal)); //add bullet hole
                Destroy(impactPoint, 3f);      //destroy bullet hole
            }

        }
    }

    IEnumerator ReloadWeapon()
    {
        gunData.reloading = true;
        gunAnimator.SetBool("RELOADING", true);

        yield return new WaitForSeconds(gunData.reloadTime);
        //following lines will be executed only after reloadTime seconds
        gunAnimator.SetBool("RELOADING", false);
        gunData.currentAmmo = gunData.magazineSize;
        gunData.reloading = false;
    }

  

    /*bool CheckFireButton() {          //OPTIONAL, if we want to make semi automatic fire
    if (gunData.isAutomatic == true)
    {
        if (Input.GetButton("Fire1"))
            return true;
    }
    else if (gunData.isAutomatic == false) {
        if (Input.GetButtonDown("Fire1"))
            return true;
    }
    return false;
}*/

}
