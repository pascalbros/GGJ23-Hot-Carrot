using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;

    private Vector2 currentInput;

    void Start()
    {
        
    }

    void Update() {
        transform.Translate(currentInput * Time.deltaTime * speed);

    }

    public void Move(InputAction.CallbackContext context) {
        currentInput = context.ReadValue<Vector2>();
    }

    public void Turbo(InputAction.CallbackContext context) {
        if (!context.started) { return; }
        Debug.Log("TODO: Turbo");
    }
}
