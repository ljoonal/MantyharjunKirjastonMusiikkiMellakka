using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent: UnityEvent<float, float> { }

public class InputController : MonoBehaviour
{
    InputControls controls;
    public MoveInputEvent moveInputEvent;

    private void Awake()
    {
        controls = new InputControls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Move.performed += OnMovePerformed;
        controls.Gameplay.Move.canceled += OnMovePerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        // -1 because game is "wrong way"
        //moveInputEvent.Invoke(moveInput.x * -1, moveInput.y * -1);
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
        Debug.Log($"Move Input: {moveInput}");
    }
}
