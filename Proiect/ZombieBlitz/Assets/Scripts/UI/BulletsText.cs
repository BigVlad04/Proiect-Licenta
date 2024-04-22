using TMPro;
using UnityEngine;

public class BulletsText : MonoBehaviour
{
    public TextMeshProUGUI bulletsText;
    public GameObject weaponHolder;
    public weaponsEnum weapon;
    GunData pistolData;
    GunData AKData;
    GunData ShotgunData;
    GunData SniperData;
    void Start()
    {
        GunData[] guns = new GunData[4];
        guns = Resources.FindObjectsOfTypeAll<GunData>();
        foreach (GunData gun in guns)
        {
            if (gun.weaponName.Equals("Pistol"))
            {
                pistolData=gun;
            }
            if (gun.weaponName.Equals("AK47"))
            {
                AKData=gun;
            }
            if (gun.weaponName.Equals("Shotgun"))
            {
                ShotgunData=gun;
            }
            if (gun.weaponName.Equals("Sniper"))
            {
                SniperData=gun;
            }
        }
        
    }

    void Update()
    {
        int currentAmmo;
        int magazineSize;
        weapon = weaponHolder.GetComponent<WeaponSwitching>().getSelectedWeapon();
        switch (weapon)
        {
            case weaponsEnum.PISTOL:
                currentAmmo = pistolData.currentAmmo;
                magazineSize = pistolData.magazineSize;
                break;
            case weaponsEnum.AK47:
                currentAmmo = AKData.currentAmmo;
                magazineSize = AKData.magazineSize;
                break;
            case weaponsEnum.SHOTGUN:
                currentAmmo = ShotgunData.currentAmmo;
                magazineSize = ShotgunData.magazineSize;
                break;
            case weaponsEnum.SNIPER:
                currentAmmo = SniperData.currentAmmo;
                magazineSize = SniperData.magazineSize;
                break;
            default:
                currentAmmo = 0;
                magazineSize = 0;
                break;
        }
        DisplayText(currentAmmo, magazineSize);
    }

    void DisplayText(int currentAmmo, int magazineSize) { 
        bulletsText.text = ":" + currentAmmo + "/" + magazineSize;
    }
}