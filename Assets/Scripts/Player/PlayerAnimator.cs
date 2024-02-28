using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private const string IS_RUNNING = "isRunning";
    private const string IS_SPRINTING = "isSprinting";
    private const string IS_BLOCKING = "isBlocking";
    private const string JUMP = "jump";
    private const string PUNCH = "punch";
    private const string DIE = "die";
    private const string ONE_HAND_SLASH = "oneHandSlash";
    private const string TWO_HAND_SLASH = "twoHandSlash";
    private Animator animator;
    private Player player;
    private PlayerBattle playerBattle;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerBattle = GetComponent<PlayerBattle>();
        playerBattle.OnAttackPerformed += PlayerBattleOnAttackPerformed;
        player.OnJumpPerformed += PlayerOnJumpPerformed;
        player.OnDeath += PlayerOnDeath;
    }

    private void PlayerOnDeath(object sender, EventArgs e)
    {
        AnimateDeath();
    }

    private void PlayerBattleOnAttackPerformed(object sender, EventArgs e)
    {
        AnimateAttack();
    }

    private void PlayerOnJumpPerformed(object sender, EventArgs e)
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
        animator.SetBool(IS_BLOCKING, playerBattle.IsBlocking());
        animator.SetBool(IS_RUNNING, player.IsRunning());
        animator.SetBool(IS_SPRINTING, player.IsSprinting());
    }

    private void AnimateJump()
    {
        animator.SetTrigger(JUMP);
    }

    private void AnimateAttack()
    {
        switch (playerBattle.GetWeaponR().weaponType)
        {
            case WeaponSO.WeaponType.Fist:
                AnimatePunch();
                break;
            case WeaponSO.WeaponType.Sword:
                AnimateOneHandSlash();
                break;
            case WeaponSO.WeaponType.BigSword:
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