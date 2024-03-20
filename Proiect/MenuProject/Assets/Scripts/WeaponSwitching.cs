using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
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
