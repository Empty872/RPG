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
        PlayerWeaponManager.Instance.OnEquipWeapon += InstanceOnEquipWeapon;
        PlayerWeaponManager.Instance.OnUnEquipWeapon += InstanceOnUnEquipWeapon;
    }

    private void InstanceOnEquipWeapon(object sender, PlayerWeaponManager.OnEquipWeaponEventArgs e)
    {
        EquipWeapon(e.weaponSO);
    }

    private void InstanceOnUnEquipWeapon(object sender, PlayerWeaponManager.OnUnEquipWeaponEventArgs e)
    {
        UnEquipWeapon(e.weaponArmType);
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

        Debug.DrawRay(attackSource.position, attackSource.forward * weaponR.Distance, Color.red);
    }

    public void TryMakeDamage()
    {
        wasHit = true;
        if (Physics.Raycast(attackSource.position, attackSource.forward,
                out RaycastHit hit, weaponR.Distance))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(weaponR.Damage);
            }
        }
    }

    public void TryAttack()
    {
        currentTimeToHit = weaponR.TimeToHit;
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
        timeToAttack = weaponR.AttackCooldown;
        // TryMakeDamage();
    }

    public WeaponSO GetWeaponL()
    {
        return weaponL;
    }

    public WeaponSO GetWeaponR()
    {
        return weaponR;
    }


    public void EquipWeapon(WeaponSO newWeapon)
    {
        if (weaponR.WeaponArmType == WeaponArmType.BothHanded)
        {
            weaponL = fist;
            weaponR = fist;
        }

        switch (newWeapon.WeaponArmType)
        {
            case WeaponArmType.BothHanded:
                weaponR = newWeapon;
                weaponL = fist;
                break;
            case WeaponArmType.LeftHanded:
                weaponL = newWeapon;
                break;
            case WeaponArmType.RightHanded:
                weaponR = newWeapon;
                break;
        }
    }

    public void UnEquipWeapon(WeaponArmType weaponArmType)
    {
        switch (weaponArmType)
        {
            case WeaponArmType.LeftHanded:
                weaponL = fist;
                break;
            default:
                weaponR = fist;
                break;
        }
    }

    public bool IsAttacking()
    {
        return timeToAttack > 0;
    }

    public bool IsBlocking()
    {
        return GameInput.Instance.IsBlockButtonPressed() && weaponL.WeaponType == WeaponType.Shield &&
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