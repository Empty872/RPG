using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private const string IS_RUNNING = "isRunning";
    private const string IS_SPRINTING = "isSprinting";
    private const string JUMP = "jump";
    private const string PUNCH = "punch";
    private const string DIE = "die";
    private const string ONE_HAND_SLASH = "oneHandSlash";
    private const string TWO_HAND_SLASH = "twoHandSlash";
    private Animator animator;
    private Enemy enemy;
    private EnemyBattle enemyBattle;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyBattle = GetComponent<EnemyBattle>();
        enemyBattle.OnAttackPerformed += EnemyBattleOnAttackPerformed;
        enemy.OnJumpPerformed += EnemyOnJumpPerformed;
        enemy.OnDeath += EnemyOnDeath;
        // gameInput.OnJump += GameInputOnJump;
    }

    private void EnemyOnDeath(object sender, EventArgs e)
    {
        AnimateDeath();
        enemy.OnDeath -= EnemyOnDeath;
        // animator.enabled = false;
    }

    private void EnemyBattleOnAttackPerformed(object sender, EventArgs e)
    {
        AnimateAttack();
    }

    private void EnemyOnJumpPerformed(object sender, EventArgs e)
    {
        AnimateJump();
    }
    // private void GameInputOnJump(object sender, EventArgs e)
    // {
    //     AnimateJump();
    // }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_RUNNING, enemy.IsRunning());
        animator.SetBool(IS_SPRINTING, enemy.IsSprinting());
    }

    private void AnimateJump()
    {
        animator.SetTrigger(JUMP);
    }

    private void AnimateAttack()
    {
        switch (enemyBattle.GetWeaponSO().WeaponType)
        {
            case WeaponType.Fist:
                AnimatePunch();
                break;
            case WeaponType.Sword:
                AnimateOneHandSlash();
                break;
            case WeaponType.BigSword:
                AnimateTwoHandSlash();
                break;
        }
    }

    private void AnimatePunch()
    {
        animator.SetTrigger(PUNCH);
    }


    private void AnimateOneHandSlash()
    {
        animator.SetTrigger(ONE_HAND_SLASH);
    }

    private void AnimateTwoHandSlash()
    {
        animator.SetTrigger(TWO_HAND_SLASH);
    }

    private void AnimateDeath()
    {
        animator.SetTrigger(DIE);
    }
}