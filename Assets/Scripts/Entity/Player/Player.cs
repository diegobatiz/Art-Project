using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerAttack _attack;

    public bool IsJumping()
    {
        return _movement.IsJumping;
    }

    public bool IsAttacking()
    {
        return _attack.IsAttacking;
    }

    public void OnDamaged(Action<float> action)
    {
        _health.OnDamaged += action;
    }

    public static void PauseMovement()
    {
        PlayerMovement.PauseMovement = true;
    }

    public static void UnpauseMovement()
    {
        PlayerMovement.PauseMovement = false;
    }
}
