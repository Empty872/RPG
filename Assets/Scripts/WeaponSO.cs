using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] private Transform weaponPrefab;
    [SerializeField] private Transform weaponUIPrefab;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private float damage;
    [SerializeField] private float distance;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float timeToHit;
    [SerializeField] private string name;
    [SerializeField] private int cost;

    public Transform WeaponPrefab => weaponPrefab;
    public Transform WeaponUIPrefab => weaponUIPrefab;
    public float Damage => damage;
    public float Distance => distance;
    public float AttackCooldown => attackCooldown;
    public float TimeToHit => timeToHit;

    public WeaponType WeaponType => weaponType;
    public string Name => name;
    public int Cost => cost;

    // public WeaponArmType WeaponArmType => GetArmType();
    public WeaponArmType WeaponArmType
    {
        get
        {
            switch (weaponType)
            {
                case WeaponType.Fist:
                    return WeaponArmType.RightHanded;
                case WeaponType.Sword:
                    return WeaponArmType.RightHanded;
                case WeaponType.Shield:
                    return WeaponArmType.LeftHanded;
                case WeaponType.BigSword:
                    return WeaponArmType.BothHanded;
            }

            return WeaponArmType.RightHanded;
        }
    }
}