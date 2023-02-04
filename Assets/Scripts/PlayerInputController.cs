using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DavidJalbert;

public class PlayerInputController : MonoBehaviour
{
    public TinyCarController carController;

    public Vector2 moveInput;
    public bool isAccelerating;
    public bool boostInput;

    [Header("Input")]
    [Tooltip("For how long the boost should last in seconds.")]
    public float boostDuration = 1;
    [Tooltip("How long to wait after a boost has been used before it can be used again, in seconds.")]
    public float boostCoolOff = 0;
    [Tooltip("The value by which to multiply the speed and acceleration of the car when a boost is used.")]
    public float boostMultiplier = 2;

    private float boostTimer = 0;

    void Start() {
        MatchMaker.current.OnPlayerAdded(gameObject);
    }

    void Update() {
        float motorDelta = moveInput.y;
        float steeringDelta = moveInput.x;
        if (boostInput && boostTimer == 0) {
            boostTimer = boostCoolOff + boostDuration;
        } else if (boostTimer > 0) {
            boostTimer = Mathf.Max(boostTimer - Time.deltaTime, 0);
            carController.setBoostMultiplier(boostTimer > boostCoolOff ? boostMultiplier : 1);
        }

        carController.setSteering(steeringDelta);
        carController.setMotor(motorDelta);
    }

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        if (isAccelerating) {
            moveInput.y = 1;
        }
    }

    public void Accelerate(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            isAccelerating = true;
        } else if (context.phase == InputActionPhase.Canceled) {
            isAccelerating = false;
        }
        moveInput.y = isAccelerating ? 1 : 0;
    }

    public void Turbo(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            boostInput = true;
        } else if (context.phase == InputActionPhase.Canceled) {
            boostInput = false;
        }
    }
}
