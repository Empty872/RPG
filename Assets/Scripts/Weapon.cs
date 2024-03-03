using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    public WeaponSO GetWeaponSO()
    {
        return weaponSO;
    }
}