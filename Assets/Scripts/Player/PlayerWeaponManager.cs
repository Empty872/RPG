using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance { get; private set; }
    public event EventHandler<OnEquipWeaponEventArgs> OnEquipWeapon;
    public event EventHandler<OnUnEquipWeaponEventArgs> OnUnEquipWeapon;

    public class OnEquipWeaponEventArgs : EventArgs
    {
        public WeaponSO weaponSO;
    }

    public class OnUnEquipWeaponEventArgs : EventArgs
    {
        public WeaponArmType weaponArmType;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EquipWeapon(StaticWeaponManager.weaponSOLeft);
        EquipWeapon(StaticWeaponManager.weaponSORight);
    }


    public void EquipWeapon(WeaponSO weaponSO)
    {
        if (weaponSO is null) return;
        //test code
        StaticWeaponManager.RememberWeapon(weaponSO);

        OnEquipWeapon?.Invoke(this, new OnEquipWeaponEventArgs { weaponSO = weaponSO });
    }

    public void UnEquipWeapon(WeaponArmType weaponArmType)
    {
        //test code
        StaticWeaponManager.ForgotWeapon(weaponArmType);

        OnUnEquipWeapon?.Invoke(this, new OnUnEquipWeaponEventArgs { weaponArmType = weaponArmType });
    }
}