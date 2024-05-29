using UnityEngine;
/// <summary>
/// This script handles weapon switching
/// </summary>
public class WeaponSwitching : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip weaponSwitch;
    public weaponsEnum selectedWeapon;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selectedWeapon = weaponsEnum.PISTOL;    //initially the pistol is equiped
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        bool weaponChanged = false;

        //switching through mouse wheel
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

        //switching through numpad keys
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
               audioSource.PlayOneShot(weaponSwitch);
            }
            else {
                weapon.gameObject.SetActive(false);
            }
                gun++;
        }
    }

    public weaponsEnum getSelectedWeapon()
    {
        return selectedWeapon;
    }
}
