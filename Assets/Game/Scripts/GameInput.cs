using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions inputActions;

    public event EventHandler OnJumpAction;
    public event EventHandler OnJumpHold;
    public event EventHandler OnJumpRelease;

    public event EventHandler OnTestPress;

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        inputActions.Enable();

        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Jump.started += Jump_started;
        inputActions.Player.Jump.canceled += Jump_canceled;

        inputActions.Player.Test.performed += Test_performed;
    }

    private void Test_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnTestPress?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpRelease?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpHold?.Invoke(this, EventArgs.Empty);
    }
}
