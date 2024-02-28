using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    // public enum WeaponType
    // {
    //     Fist,
    //     Sword,
    //     BigSword,
    //     Shield
    // }
    //
    // [SerializeField] public readonly WeaponType weaponType;
    // [SerializeField] public readonly float damage;
    // [SerializeField] public readonly float distance;
    // [SerializeField] public readonly float attackCooldown;
    // [SerializeField] public readonly float timeToHit;
    //
    // public Weapon(WeaponType weaponType)
    // {
    //     this.weaponType = weaponType;
    //     switch (this.weaponType)
    //     {
    //         case WeaponType.Fist:
    //             damage = 1;
    //             distance = 1.1f;
    //             attackCooldown = 1;
    //             timeToHit = 0.5f;
    //             break;
    //         case WeaponType.Sword:
    //             damage = 2;
    //             distance = 1.3f;
    //             attackCooldown = 1;
    //             timeToHit = 0.3f;
    //             break;
    //         case WeaponType.BigSword:
    //             damage = 4;
    //             distance = 2;
    //             attackCooldown = 2;
    //             timeToHit = 0.8f;
    //             break;
    //     }
    // }

    public WeaponSO GetWeaponSO()
    {
        return weaponSO;
    }
}