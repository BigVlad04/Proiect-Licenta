using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 2f;
    private float nextFireTime =0f;

    public Camera playerCam;

    public int magazineSize = 8;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool currentlyReloading = false;

    public Animator animator;

    private void Start()
    {
        currentAmmo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyReloading)
        {
            return;
        }

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadWeapon());
            Debug.Log("Reloading....");
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f/firerate;   
            Shoot();
        }
    }

    private void OnEnable()
    {
        currentlyReloading = false ;
        animator.SetBool("Reloading", false);
    }

    IEnumerator ReloadWeapon()
    {
        currentlyReloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);
        currentAmmo = magazineSize;
        currentlyReloading = false;
    }

    void Shoot()
    {

        RaycastHit hit;
        currentAmmo--;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            TargetScript targetScript = hit.transform.GetComponent<TargetScript>();
            if (targetScript != null)
            {
                targetScript.TakeDamage(damage);
            }
        }


    }
}
