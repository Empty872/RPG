using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private Quaternion cameraRotation = Quaternion.Euler(0, 0, 45);
    private Rigidbody rb;
    private bool canJump;
    private bool isRunning;
    private bool isSprinting;
    private float rotationSpeed = 10f;
    private float jumpForce = 5f;
    [SerializeField] private float healthPointsMax = 20f;
    private float healthPoints;
    private PlayerBattle playerBattle;
    public event EventHandler OnJumpPerformed;
    public event EventHandler OnDeath;
    public static Player Instance { get; private set; }
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

    public class OnHealthChangedEventArgs : EventArgs
    {
        public float normalizedHeath;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameInput = GameInput.Instance;
        gameInput.OnJumpButtonPressed += GameInputOnJumpButtonPressed;
        gameInput.OnAttackButtonPressed += GameInputOnAttackButtonPressed;
        gameInput.OnInteractButtonPressed += GameInputOnInteractButtonPressed;
        playerBattle = GetComponent<PlayerBattle>();
        healthPoints = healthPointsMax;
    }

    private void GameInputOnJumpButtonPressed(object sender, EventArgs e)
    {
        TryJump();
    }

    private void GameInputOnAttackButtonPressed(object sender, EventArgs e)
    {
        playerBattle.TryAttack();
    }

    private void GameInputOnInteractButtonPressed(object sender, EventArgs e)
    {
        Interact();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove()) return;
        Move();
        canJump = Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }

    private void Move()
    {
        var inputVector = cameraRotation * gameInput.GetPlayerMovementNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        // transform.position += moveDir * moveSpeed * Time.deltaTime;
        isRunning = moveDir != Vector3.zero;
        isSprinting = isRunning && gameInput.IsSprintButtonPressed();
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        var currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        transform.position += moveDir * currentSpeed * Time.deltaTime;
        // if (!playerBattle.IsAttacking())
        // {
        //     transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        // }
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
        if (!canJump) return;
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        OnJumpPerformed?.Invoke(this, EventArgs.Empty);
        // transform.position += new Vector3(0, 1, 0);
    }


    public void Interact()
    {
    }

    public void TakeDamage(float damage)
    {
        if (IsDead()) return;
        healthPoints -= damage;
        OnHealthChanged?.Invoke(this,
            new OnHealthChangedEventArgs { normalizedHeath = healthPoints / healthPointsMax });
        Debug.Log(healthPoints);
        if (healthPoints <= 0) Die();
    }

    public bool IsDead()
    {
        return healthPoints <= 0;
    }

    private void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        gameInput.Disable();
    }

    public bool CanMove()
    {
        return !(
            IsDead()
            || playerBattle.IsAttacking()
            || playerBattle.IsBlocking()
        );
    }
}