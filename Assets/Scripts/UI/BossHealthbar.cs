using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private EnemyHealth _enemy;
    private float _healthTotal;
    private float _currentHealth;

    private void Awake()
    {
        _healthTotal = _enemy.MaxHealth;
        _currentHealth = _healthTotal;
        _enemy.OnDamaged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float damage)
    {
        _currentHealth -= damage;
        float amt = _currentHealth / _healthTotal;
        _healthFill.fillAmount = amt;
    }
}
