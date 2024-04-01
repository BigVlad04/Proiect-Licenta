using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public AudioClip weaponSwitch;

    public weaponsEnum selectedWeapon = weaponsEnum.PISTOL;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        bool weaponChanged = false;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            selectedWeapon++;
            if (selectedWeapon > weaponsEnum.SNIPER) { 
                selectedWeapon = weaponsEnum.PISTOL;
            }
            weaponChanged = true;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon--;
            if (selectedWeapon < weaponsEnum.PISTOL)
            {
                selectedWeapon = weaponsEnum.SNIPER;
            }
            weaponChanged = true;
        }
        if (Input.GetKey(KeyCode.Alpha1) && selectedWeapon != weaponsEnum.PISTOL)
        {
            selectedWeapon = weaponsEnum.PISTOL;
            weaponChanged=true;
        }
        if (Input.GetKey(KeyCode.Alpha2) && selectedWeapon != weaponsEnum.AK47)
        {
            selectedWeapon = weaponsEnum.AK47;
            weaponChanged = true;
        }
        if (Input.GetKey(KeyCode.Alpha3) && selectedWeapon != weaponsEnum.SHOTGUN)
        {
            selectedWeapon = weaponsEnum.SHOTGUN;
            weaponChanged = true;
        }
        if (Input.GetKey(KeyCode.Alpha4) && selectedWeapon != weaponsEnum.SNIPER)
        {
            selectedWeapon = weaponsEnum.SNIPER;
            weaponChanged = true;
        }
        if (weaponChanged) {
            SelectWeapon();
        }
    }

    void SelectWeapon() {
        weaponsEnum gun = weaponsEnum.PISTOL;
        foreach (Transform weapon in transform) {
            if (gun == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.GetComponent<AudioSource>().PlayOneShot(weaponSwitch);
            }
            else {
                weapon.gameObject.SetActive(false);
            }
                gun++;
        }
    }

    public enum weaponsEnum{ 
        PISTOL,
        AK47,
        SHOTGUN,
        SNIPER
    }
}
