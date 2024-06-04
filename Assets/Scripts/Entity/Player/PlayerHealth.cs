using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private float _invincTime;
    [SerializeField] private float _noMoveTime;
    [SerializeField] private float _startFreezeTime;
    [SerializeField] private float _freezeTime;

    private bool _noMove;
    private bool _wasDamaged;
    private bool _freeze;
    private bool _startFreeze;

    private Timer _invincibilityTimer;
    private Timer _noMoveTimer;
    private Timer _startFreezeTimer;
    private Timer _freezeTimer;

    private Vector2 _lastVelocity = Vector2.zero;

    public event Action<float> OnDamaged;

    private void Awake()
    {
        _health = _maxHealth;

        _invincibilityTimer = new Timer(_invincTime);
        _invincibilityTimer.OnTimerEnd += ResetInvincTimer;

        _noMoveTimer = new Timer(_noMoveTime);
        _noMoveTimer.OnTimerEnd += ResetNoMoveTimer;

        _startFreezeTimer = new Timer(_startFreezeTime);
        _startFreezeTimer.OnTimerEnd += StartFreeze;

        _freezeTimer = new Timer(_freezeTime);
        _freezeTimer.OnTimerEnd += ResetFreezeTimer;
    }

    private void Update()
    {
        if (_wasDamaged)
        {
            if (_startFreeze)
            {
                _startFreezeTimer.Tick(Time.deltaTime);
            }
            else if (_freeze)
            {
                _freezeTimer.Tick(Time.deltaTime);
            }    

            if (_noMove)
            {
                _noMoveTimer.Tick(Time.deltaTime);
            }

            _invincibilityTimer.Tick(Time.deltaTime);
            _lastVelocity = _rb2d.velocity;
        }
    }

    public bool Damage(float amt)
    {
        if (_wasDamaged)
        {
            return false;
        }

        Debug.Log("Damaged");

        _startFreeze = true;
        _noMove = true;
        _wasDamaged = true;
        _health -= amt;

        PlayerMovement.PauseMovement = true;

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

    private void ResetInvincTimer()
    {
        Debug.Log("reset invinc");
        _invincibilityTimer.ResetTimer();
        _wasDamaged = false;
    }

    private void ResetNoMoveTimer()
    {
        Debug.Log("reset no move");
        _noMoveTimer.ResetTimer();
        _noMove = false;
        PlayerMovement.PauseMovement = false;
    }

    private void StartFreeze()
    {
        _rb2d.velocity = Vector2.zero;
        _rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        _freeze = true;
        _startFreeze = false;
        _startFreezeTimer.ResetTimer();
        Debug.Log("start freeze");
    }

    private void ResetFreezeTimer()
    {
        Debug.Log("reset freeze");
        _freezeTimer.ResetTimer();
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        _freeze = false;
    }
}
