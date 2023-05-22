using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    //Static
    public static InputReader Instance;

    //Vector2 Values
    public Vector2 MovementValues { get; private set; }
    public Vector2 MousePositionValue { get; private set; }
    public Vector2 MouseDelta { get; private set; }

    //Bools
    public bool IsLeftMouseButtonClicked { get; private set; }

    //Actions
    public Action LeftClickEvent;

    //Scripts
    private Controls controls;

    private void Awake() => Instance = this;

    private void Start()
    {
        controls = new();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy() => controls.Player.Disable();

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValues = context.ReadValue<Vector2>();
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePositionValue = context.ReadValue<Vector2>();
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsLeftMouseButtonClicked = true;
            LeftClickEvent?.Invoke();
        }
        if (context.canceled)
            IsLeftMouseButtonClicked = false;

    }
}
