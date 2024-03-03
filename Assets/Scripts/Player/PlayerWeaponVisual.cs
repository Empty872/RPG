using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponVisual : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bigSword;
    [SerializeField] private GameObject shield;

    void Start()
    {
        PlayerWeaponManager.Instance.OnEquipWeapon += InstanceOnEquipWeapon;
        PlayerWeaponManager.Instance.OnUnEquipWeapon += InstanceOnUnEquipWeapon;
    }

    private void InstanceOnEquipWeapon(object sender, PlayerWeaponManager.OnEquipWeaponEventArgs e)
    {
        ShowWeapon(e.weaponSO);
    }

    private void InstanceOnUnEquipWeapon(object sender, PlayerWeaponManager.OnUnEquipWeaponEventArgs e)
    {
        HideWeapon(e.weaponArmType);
    }

    public void HideWeaponsRight()
    {
        sword.SetActive(false);
        bigSword.SetActive(false);
    }

    public void HideWeaponsLeft()
    {
        shield.SetActive(false);
        bigSword.SetActive(false);
    }

    public void ShowWeapon(WeaponSO weaponSO)
    {
        switch (weaponSO.WeaponType)
        {
            case WeaponType.Sword:
                HideWeaponsRight();
                sword.SetActive(true);
                break;
            case WeaponType.BigSword:
                HideWeaponsRight();
                HideWeaponsLeft();
                bigSword.SetActive(true);
                break;
            case WeaponType.Shield:
                HideWeaponsLeft();
                shield.SetActive(true);
                break;
            case WeaponType.Fist:
                HideWeaponsRight();
                HideWeaponsLeft();
                break;
        }
    }

    public void HideWeapon(WeaponArmType weaponArmType)
    {
        switch (weaponArmType)
        {
            case WeaponArmType.LeftHanded:
                HideWeaponsLeft();
                break;
            case WeaponArmType.RightHanded:
                HideWeaponsRight();
                break;
            case WeaponArmType.BothHanded:
                HideWeaponsLeft();
                HideWeaponsRight();
                break;
        }
    }
}