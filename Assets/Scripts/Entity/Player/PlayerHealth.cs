using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private Vector3 _respawnPoint;

    protected override void Death()
    {
        _health = _maxHealth;
        transform.position = _respawnPoint;
    }
}
