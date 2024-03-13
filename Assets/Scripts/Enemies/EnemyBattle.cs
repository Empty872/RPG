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

        Debug.DrawRay(attackSource.position, attackSource.forward * weaponSO.Distance, Color.red);
    }

    public void TryMakeDamage()
    {
        wasHit = true;
        if (Physics.Raycast(attackSource.position, attackSource.forward,
                out RaycastHit hit, weaponSO.Distance))
        {
            // Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out Player player))
            {
                player.TakeDamage(weaponSO.Damage);
            }
        }
    }

    public void TryAttack()
    {
        if (IsAttacking()) return;
        currentTimeToHit = weaponSO.TimeToHit;
        wasHit = false;
        transform.LookAt(Player.Instance.transform);
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
        timeToAttack = weaponSO.AttackCooldown;
    }


    public bool IsAttacking()
    {
        return timeToAttack > 0;
    }

    public float GetAttackDistance()
    {
        return weaponSO.Distance;
    }

    public WeaponSO GetWeaponSO()
    {
        return weaponSO;
    }
}