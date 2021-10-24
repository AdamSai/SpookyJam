using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 moveDirection { get; set; }
    public Vector2 lookDirection { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
            lookDirection = context.ReadValue<Vector2>();
    }
}
