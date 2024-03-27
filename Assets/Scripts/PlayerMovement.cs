using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    float _direction;

    protected override float GetDirection()
    {
        return _direction;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().x;
    }
}
