using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Vector3 _respawnPoint;

    public event Action OnDamaged;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void Damage(float amt)
    {
        _health -= amt;
        if (_health <= 0f)
        {
            Death();
        }

        OnDamaged?.Invoke();
    }

    public void Death()
    {
        _health = _maxHealth;
        transform.position = _respawnPoint;
    }
}
