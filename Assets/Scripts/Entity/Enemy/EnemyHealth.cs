using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float KnockbackMultiplier = 1.0f;
    public float MaxHealth;
    private float _health;

    public event Action<float> OnDamaged;
    public event Action OnDeath;

    void Awake()
    {
        _health = MaxHealth;
    }

    public bool Damage(float amt)
    {
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
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
