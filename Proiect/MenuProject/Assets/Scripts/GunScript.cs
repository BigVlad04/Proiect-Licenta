using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GunData gunData;
   /* public float damage = 10f;
    public float range = 100f;
    public float firerate = 2f;*/
    private float nextFireTime = 0f;

    public Camera playerCam;

   /* public int magazineSize = 8;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool currentlyReloading = false;*/

    public Animator animator;

    private void Start()
    {
        gunData.currentAmmo = gunData.magazineSize;
       // currentAmmo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunData.reloading)
        {
            return;
        }

        if (gunData.currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadWeapon());
            Debug.Log("Reloading....");
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / gunData.fireRate;
            Shoot();
        }
    }

    private void OnEnable()
    {
        gunData.reloading= false;
        animator.SetBool("Reloading", false);
    }

    IEnumerator ReloadWeapon()
    {
        gunData.reloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(gunData.reloadTime);

        animator.SetBool("Reloading", false);
        gunData.currentAmmo = gunData.magazineSize;
        gunData.reloading = false;
    }

    void Shoot()
    {

        RaycastHit hit;
        gunData.currentAmmo--;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, gunData.range))
        {
            Debug.Log(hit.transform.name + " " + gunData.damage);
            TargetScript targetScript = hit.transform.GetComponent<TargetScript>();
            if (targetScript != null)
            {
                targetScript.TakeDamage(gunData.damage);
            }
        }


    }
}
