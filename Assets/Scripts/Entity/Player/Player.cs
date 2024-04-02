using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerHealth _health;

    public bool IsJumping()
    {
        return _movement.IsJumping;
    }
}
