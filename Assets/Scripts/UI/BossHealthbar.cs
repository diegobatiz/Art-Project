using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private EnemyHealth _enemy;

    private void Awake()
    {
        _enemy.OnDamaged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float damage)
    {
        _healthFill.fillAmount -= damage * 0.01f;
    }
}
