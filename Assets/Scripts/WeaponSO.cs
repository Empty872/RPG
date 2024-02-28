using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    // Start is called before the first frame update
    public Transform prefab;
    public enum WeaponType
    {
        Fist,
        Sword,
        BigSword,
        Shield
    }

    public WeaponType weaponType;
    public float damage;
    public float distance;
    public float attackCooldown;
    public float timeToHit;

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
}