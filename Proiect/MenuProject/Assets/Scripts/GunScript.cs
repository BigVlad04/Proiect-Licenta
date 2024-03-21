using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 15f;
    private float nextFireTime =0f;

    public Camera playerCam;

    public int magazineSize = 8;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool currentlyReloading = false;

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

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f/firerate;
            if (currentAmmo <= 0)
            {
                StartCoroutine(ReloadWeapon());
                return;
            }
            Shoot();
        }
    }

    IEnumerator ReloadWeapon()
    {
        currentlyReloading = true;
        yield return new WaitForSeconds(reloadTime);
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
