using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed = 7f;
    private Rigidbody rb;
    private bool canJump;
    private bool isRunning;
    private bool isSprinting;
    [SerializeField] private float healthPointsMax = 5f;
    private float healthPoints;
    private float rotationSpeed = 10f;
    private float jumpForce = 5f;
    private EnemyBattle enemyBattle;
    private FieldOfView fieldOfView;
    public event EventHandler OnJumpPerformed;
    public event EventHandler OnDeath;
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    private State state;
    private Transform playerTransform;

    public enum State
    {
        Idle,
        ChasingAtPlayer,
        AttackingPlayer,
        Dead
    }

    public class OnHealthChangedEventArgs : EventArgs
    {
        public float normalizedHeath;
    }

    void Start()
    {
        playerTransform = Player.Instance.transform;
        state = State.Idle;
        rb = GetComponent<Rigidbody>();
        enemyBattle = GetComponent<EnemyBattle>();
        fieldOfView = GetComponent<FieldOfView>();
        healthPoints = healthPointsMax;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                isRunning = false;
                break;
            case State.Dead:
                break;
            case State.ChasingAtPlayer:
                Move();
                break;
            case State.AttackingPlayer:
                isRunning = false;
                enemyBattle.TryAttack();
                break;
        }

        // if (IsDead()) return;
        // enemyBattle.TryAttack();
        // Move();
        // canJump = Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }

    private void Move()
    {
        if (enemyBattle.IsAttacking()) return;
        var vectorToPlayer = playerTransform.position - transform.position;
        var moveDir = new Vector3(vectorToPlayer.x, 0f, vectorToPlayer.z).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isRunning = true;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        // isSprinting = isRunning && gameInput.IsSprintButtonPressed();
        // if (!enemyBattle.IsAttacking())
        // {
        //     transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        // }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        OnHealthChanged?.Invoke(this,
            new OnHealthChangedEventArgs { normalizedHeath = healthPoints / healthPointsMax });
        fieldOfView.DetectPlayer();
        if (healthPoints <= 0)
        {
            Die();
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsSprinting()
    {
        return isSprinting;
    }

    public void TryJump()
    {
        // if (!canJump) return;
        // rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        // OnJumpPerformed?.Invoke(this, EventArgs.Empty);
        // // transform.position += new Vector3(0, 1, 0);
    }


    private void Die()
    {
        if (GetState() == State.Dead) return;
        OnDeath?.Invoke(this, EventArgs.Empty);
        TryChangeState(State.Dead);
        enabled = false;
        GetComponent<Collider>().enabled = false;
        Wallet.EarnMoneyForEnemyDeath();
    }

    private void DetectPlayer()
    {
    }

    public void TryChangeState(State newState)
    {
        state = newState;
    }

    public State GetState()
    {
        return state;
    }
}