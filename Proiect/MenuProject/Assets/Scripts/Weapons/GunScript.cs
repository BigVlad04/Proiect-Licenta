using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GunData gunData;

    private float nextFireTime = 0f;


    public Camera playerCam;

    public GameObject impactEffect;



    //public Animator animator;
    public Animator gunAnimator;
    public GameObject muzzleEffect;

    public AudioSource audioSource;
    private float nextDryFireTime = 0f;
    private float dryFireTime = 0.5f;

    private void Start()
    {
        audioSource =transform.parent.GetComponent<AudioSource>();
        gunAnimator = GetComponent<Animator>();
        gunData.currentAmmo = gunData.magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunData.reloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) // || (gunData.currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)))
        {
            audioSource.PlayOneShot(gunData.reloadSound);
            StartCoroutine(ReloadWeapon());
            Debug.Log("Reloading....");
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            if (gunData.currentAmmo > 0 && Time.time >= nextFireTime )
            {
                nextFireTime = Time.time + 1f / gunData.fireRate;
                Shoot();
            }
            else if (gunData.currentAmmo == 0 && Time.time >= nextDryFireTime) {
                nextDryFireTime = Time.time + dryFireTime;
                audioSource.PlayOneShot(gunData.dryFireSound);
            }
            
        }
    }

    /*bool CheckFireButton() {          //optional, if we want to make semi automatic fire
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

    private void OnEnable()
    {
        gunData.reloading= false;
        gunAnimator.SetBool("RELOADING", false);
    }

    IEnumerator ReloadWeapon()
    {
        gunData.reloading = true;
        gunAnimator.SetBool("RELOADING", true);

        yield return new WaitForSeconds(gunData.reloadTime);

        gunAnimator.SetBool("RELOADING", false);
        gunData.currentAmmo = gunData.magazineSize;
        gunData.reloading = false;
    }

    void Shoot()
    {
        audioSource.PlayOneShot(gunData.shootingSound);
        gunAnimator.SetTrigger("RECOIL");
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        RaycastHit hit;
        gunData.currentAmmo--;
        
        
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, gunData.range))
        {
            Debug.Log(Time.time+ " " +hit.transform.name + " " + gunData.damage);
            TargetScript targetScript = hit.transform.GetComponent<TargetScript>();
            if (targetScript != null)
            {
                targetScript.TakeDamage(gunData.damage);
            }

         /*   if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * gunData.damage);           ///add impact force
            }*/

            GameObject impactPoint=Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactPoint, 2f);
        }
    }
}
