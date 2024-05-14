using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _maxHealth;
    private float _health;

    public event Action<float> OnDamaged;

    void Awake()
    {
        _health = _maxHealth;
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
        gameObject.SetActive(false);
    }
}
