public class StaticWeaponManager
{
    public static WeaponSO weaponSOLeft;
    public static WeaponSO weaponSORight;

    public static void RememberWeapon(WeaponSO weaponSO)
    {
        switch (weaponSO.WeaponArmType)
        {
            //test code
            case WeaponArmType.LeftHanded:
                weaponSOLeft = weaponSO;
                break;
            case WeaponArmType.RightHanded:
                weaponSORight = weaponSO;
                break;
            case WeaponArmType.BothHanded:
                weaponSORight = weaponSO;
                weaponSOLeft = null;
                break;
        }
    }
    public static void ForgotWeapon(WeaponArmType weaponArmType)
    {
        switch (weaponArmType)
        {
            case WeaponArmType.LeftHanded:
                weaponSOLeft = null;
                break;
            default:
                weaponSORight = null;
                break;
        }
    }
}