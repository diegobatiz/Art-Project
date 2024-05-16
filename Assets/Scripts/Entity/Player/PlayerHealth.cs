using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private float _invincTime;

    private bool _wasDamaged;
    private Timer _invincibilityTimer;

    public event Action<float> OnDamaged;

    private void Awake()
    {
        _health = _maxHealth;
        _invincibilityTimer = new Timer(_invincTime);
        _invincibilityTimer.OnTimerEnd += ResetTimer;
    }

    private void Update()
    {
        if (_wasDamaged)
        {
            _invincibilityTimer.Tick(Time.deltaTime);
        }
    }

    public bool Damage(float amt)
    {
        if (_wasDamaged)
        {
            return false;
        }

        _wasDamaged = true;
        _health -= amt;
        if (_health <= 0f)
        {
            Death();
        }

        OnDamaged?.Invoke(amt);
        return true;
    }

    public void Death()
    {
        _health = _maxHealth;
        transform.position = _respawnPoint.position;
    }

    private void ResetTimer()
    {
        _invincibilityTimer.ResetTimer();
        _wasDamaged = false;
    }
}
