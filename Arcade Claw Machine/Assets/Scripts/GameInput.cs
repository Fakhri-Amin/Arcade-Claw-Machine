using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    public event Action OnGrabPressed;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Grab.performed += Grab_Performed;
    }

    private void Grab_Performed(InputAction.CallbackContext context)
    {
        OnGrabPressed?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
