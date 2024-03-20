using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    weaponsEnum selectedWeapon = weaponsEnum.PISTOL;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
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
