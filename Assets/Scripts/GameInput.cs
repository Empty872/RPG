using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInputActions playerInputActions;
    public event EventHandler OnJumpButtonPressed;
    public event EventHandler OnAttackButtonPressed;
    public event EventHandler OnInteractButtonPressed;
    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += JumpPerformed;
        playerInputActions.Player.Attack.performed += AttackPerformed;
        playerInputActions.Player.Interact.performed += InteractionPerformed;
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        OnJumpButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void AttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttackButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void InteractionPerformed(InputAction.CallbackContext obj)
    {
        OnInteractButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetPlayerMovementNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }

    public bool IsSprintButtonPressed()
    {
        return playerInputActions.Player.Sprint.IsPressed();
    }

    public void Disable()
    {
        playerInputActions.Player.Disable();
    }

    public bool IsBlockButtonPressed()
    {
        return playerInputActions.Player.Block.IsPressed();
    }
}