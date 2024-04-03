using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _maxHealth;
    private float _health;

    void Awake()
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
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
