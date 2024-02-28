using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBattle : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeToAttack;

    // private float attackCooldown = 2;
    [SerializeField] private Transform attackSource;
    public event EventHandler OnAttackPerformed;

    [SerializeField] private WeaponSO weaponSO;

    // private Weapon.WeaponType weaponType = Weapon.WeaponType.BigSword;
    // [SerializeField] private float damage;
    // [SerializeField] private float distance;
    // [SerializeField] private float timeToHit;
    private float currentTimeToHit;
    private bool wasHit;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToAttack > 0)
        {
            timeToAttack -= Time.deltaTime;
            currentTimeToHit -= Time.deltaTime;
            if (currentTimeToHit <= 0 && !wasHit)
            {
                TryMakeDamage();
            }
        }

        Debug.DrawRay(attackSource.position, attackSource.forward * weaponSO.distance, Color.red);
    }

    public void TryMakeDamage()
    {
        wasHit = true;
        if (Physics.Raycast(attackSource.position, attackSource.forward,
                out RaycastHit hit, weaponSO.distance))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out Player player))
            {
                player.TakeDamage(weaponSO.damage);
            }
        }
    }

    public void TryAttack()
    {
        if (IsAttacking()) return;
        currentTimeToHit = weaponSO.timeToHit;
        wasHit = false;
        transform.LookAt(Player.Instance.transform);
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
        timeToAttack = weaponSO.attackCooldown;
    }

    // public Weapon.WeaponType GetWeaponType()
    // {
    //     return weaponType;
    // }

    // public void RemoveWeapons()
    // {
    //     weaponType = Weapon.WeaponType.Fist;
    //     damage = 1f;
    //     distance = 1.1f;
    //     attackCooldown = 1f;
    // }
    //
    // public void EquipWeapon(Weapon newWeapon)
    // {
    //     weaponType = newWeapon.GetWeaponType();
    //     damage = newWeapon.GetWeaponDamage();
    //     distance = newWeapon.GetWeaponDistance();
    //     attackCooldown = newWeapon.GetWeaponCooldown();
    // }

    public bool IsAttacking()
    {
        return timeToAttack > 0;
    }

    public float GetAttackDistance()
    {
        return weaponSO.distance;
    }

    public WeaponSO GetWeaponSO()
    {
        return weaponSO;
    }
}