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
        switch (weaponSO.weaponType)
        {
            case WeaponSO.WeaponType.Sword:
                HideWeaponsRight();
                sword.SetActive(true);
                break;
            case WeaponSO.WeaponType.BigSword:
                HideWeaponsRight();
                HideWeaponsLeft();
                bigSword.SetActive(true);
                break;
            case WeaponSO.WeaponType.Shield:
                HideWeaponsLeft();
                shield.SetActive(true);
                break;
            case WeaponSO.WeaponType.Fist:
                HideWeaponsRight();
                HideWeaponsLeft();
                break;
        }
    }
}