using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _player.OnDamaged(UpdateHealthBar);
    }

    private void UpdateHealthBar(float damage)
    {
        _healthFill.fillAmount -= damage * 0.01f;
    }
}
