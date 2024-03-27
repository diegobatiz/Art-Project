using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected float _health ;
    [SerializeField] protected float _maxHealth = 30f;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public virtual void Damage(float amt)
    {
        _health -= amt;
        if (_health <= 0f)
        {
            Death();
        }
    }

    protected virtual void Death()
    {

    }
}
