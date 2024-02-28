using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius;
    [Range(0, 360)] [SerializeField] private float angle;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private bool playerWasDetected;
    private EnemyBattle enemyBattle;
    private Enemy enemy;

    private void Start()
    {
        enemyBattle = GetComponent<EnemyBattle>();
        enemy = GetComponent<Enemy>();
        playerTransform = Player.Instance.transform;
        // StartCoroutine(FOVRoutine());
        enemy.OnDeath += (sender, args) => StopAllCoroutines();
    }

    // private IEnumerator FOVRoutine()
    // {
    //     WaitForSeconds wait = new WaitForSeconds(0.2f);
    //
    //     while (true)
    //     {
    //         yield return wait;
    //         FieldOfViewCheck();
    //     }
    // }

    private void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        var vectorToTarget = playerTransform.position - transform.position;
        var dirToTarget = vectorToTarget.normalized;
        var distanceToTarget = vectorToTarget.magnitude;
        canSeePlayer = distanceToTarget <= radius &&
                       !Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstructionMask) &&
                       Vector3.Angle(transform.forward, dirToTarget) < angle / 2;
        playerWasDetected = playerWasDetected && distanceToTarget <= radius || canSeePlayer;
        TryChangeState();
    }

    private void TryChangeState()
    {
        if (!playerWasDetected)
        {
            enemy.TryChangeState(Enemy.State.Idle);
            return;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= enemyBattle.GetAttackDistance())
        {
            enemy.TryChangeState(Enemy.State.AttackingPlayer);
            return;
        }

        enemy.TryChangeState(Enemy.State.ChasingAtPlayer);
    }

    public float GetRadius()
    {
        return radius;
    }

    public float GetAngle()
    {
        return angle;
    }

    public bool CanSeePlayer()
    {
        return canSeePlayer;
    }

    public bool WasPlayerDetected()
    {
        return playerWasDetected;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    public void DetectPlayer()
    {
        playerWasDetected = true;
    }
}