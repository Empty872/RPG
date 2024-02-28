using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBattle : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeToAttack;
    [SerializeField] private Transform attackSource;

    public event EventHandler OnAttackPerformed;

    // private Weapon.WeaponType weaponTypeR = Weapon.WeaponType.Fist;
    // private Weapon.WeaponType weaponTypeL = Weapon.WeaponType.Fist;
    [SerializeField] private Collider shieldCollider;
    private float currentTimeToHit;
    private bool wasHit;
    [SerializeField] private WeaponSO fist;
    private WeaponSO weaponL;
    private WeaponSO weaponR;

    private void Start()
    {
        weaponL = fist;
        weaponR = fist;
    }

    // Update is called once per frame
    void Update()
    {
        TryBlock();
        if (IsAttacking())
        {
            timeToAttack -= Time.deltaTime;
            currentTimeToHit -= Time.deltaTime;
            if (currentTimeToHit <= 0 && !wasHit)
            {
                TryMakeDamage();
            }
        }

        Debug.DrawRay(attackSource.position, attackSource.forward * weaponR.distance, Color.red);
    }

    public void TryMakeDamage()
    {
        wasHit = true;
        if (Physics.Raycast(attackSource.position, attackSource.forward,
                out RaycastHit hit, weaponR.distance))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(weaponR.damage);
            }
        }
    }

    public void TryAttack()
    {
        currentTimeToHit = weaponR.timeToHit;
        wasHit = false;
        if (IsAttacking()) return;
        shieldCollider.enabled = false;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out RaycastHit hit))
        // {
        //     transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        // };
        // RaycastHit[] hits = new RaycastHit[5];
        RaycastHit[] hits = Physics.RaycastAll(ray).OrderBy(h => h.distance).ToArray();
        foreach (var hit in hits)
        {
            // Debug.Log(hit.transform.name);
            if (hit.collider.TryGetComponent(out Player playerComponent))
            {
                continue;
            }

            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            break;
        }


        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
        timeToAttack = weaponR.attackCooldown;
        // TryMakeDamage();
    }

    // public WeaponSO.WeaponType GetWeaponType()
    // {
    //     return weaponR.weaponType;
    // }

    public WeaponSO GetWeaponL()
    {
        return weaponL;
    }

    public WeaponSO GetWeaponR()
    {
        return weaponR;
    }

    public void RemoveWeapons()
    {
        weaponR = null;
        // weaponTypeR = Weapon.WeaponType.Fist;
        // timeToHit = 0.5f;
        // damage = 1f;
        // distance = 1.1f;
        // attackCooldown = 1f;
    }

    public void EquipWeapon(WeaponSO newWeapon)
    {
        switch (newWeapon.weaponType)
        {
            case WeaponSO.WeaponType.Shield:
                weaponL = newWeapon;
                if (weaponR.weaponType == WeaponSO.WeaponType.BigSword) weaponR = fist;
                break;
            case WeaponSO.WeaponType.BigSword:
                weaponL = fist;
                break;
        }

        if (newWeapon.weaponType != WeaponSO.WeaponType.Shield)
        {
            weaponR = newWeapon;
        }
    }

    public bool IsAttacking()
    {
        return timeToAttack > 0;
    }

    public bool IsBlocking()
    {
        return GameInput.Instance.IsBlockButtonPressed() && weaponL.weaponType == WeaponSO.WeaponType.Shield &&
               !IsAttacking() && !Player.Instance.IsSprinting();
    }

    private void TryBlock()
    {
        shieldCollider.enabled = IsBlocking();
        if (!IsBlocking()) return;
        var cameraOffset = Quaternion.Euler(0, 45, 0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray).OrderBy(h => h.distance).ToArray();
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Player playerComponent))
            {
                continue;
            }

            var dir = cameraOffset *
                      (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
            var targetPoint = transform.position + dir;
            transform.LookAt(targetPoint);
            break;
        }
        // if (Physics.Raycast(ray, out RaycastHit hit))
        // {
        //     // transform.forward = Vector3.Slerp(transform.forward,
        //     //     new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position, 0);
        //     var dir = cameraOffset * (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
        //     Debug.Log(dir);
        //     var targetPoint = transform.position + dir;
        //     transform.LookAt(targetPoint);
        // }
    }
}