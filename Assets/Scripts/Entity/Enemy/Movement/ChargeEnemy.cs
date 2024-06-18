using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Movement
{
    [SerializeField] private MovementData _chargeData;
    [SerializeField] private EnemyData _aiData;
    [SerializeField] private Transform _castPoint;

    [SerializeField] private Timer _pauseTimer;
    [SerializeField] private float _pauseDuration;
    [SerializeField] private Timer _chargeTimer;
    [SerializeField] private float _chargeDuration;

    [SerializeField] private Vector2 _selfKnockBack = new();

    private EnemyAI _ai;
    private bool _startCharge = false;
    private bool _startPause = false;
    private float _chargeDirection;

    public float Direction { get; private set; } = 1f;

    protected override void Awake()
    {
        base.Awake();
        _ai = new EnemyAI(_aiData.Range, _castPoint, transform, _aiData.LayerMask, _aiData.FocusDuration);

        _pauseTimer = new Timer(_pauseDuration);
        _pauseTimer.OnTimerEnd += OnPauseTimerEnd;

        _chargeTimer = new Timer(_chargeDuration);
        _chargeTimer.OnTimerEnd += OnChargeTimerEnd;
    }

    protected override float GetDirection()
    {
        if (!_startPause && _ai.DetectPlayerCheck())
        {
            Debug.Log("Player Detected");
            _pauseTimer.ResetTimer();
            _startPause = true;
            Direction = _ai.PlayerDirection;
            Flip(Direction);

            return 0;
        }
        else if (_startPause && !_startCharge)
        {
            _pauseTimer.Tick(Time.deltaTime);

            if (_ai.PlayerDirection != Direction)
            {
                Direction = _ai.PlayerDirection;
                Flip(Direction);
            }

            return 0;
        }
        else if (_startCharge)
        {
            _chargeTimer.Tick(Time.deltaTime);
            return Direction;
        }
        else
        {
            return Direction;
        }
    }

    private void OnPauseTimerEnd()
    {
        Debug.Log("Start Charge");
        _maxSpeed = _chargeData.MaxSpeed;
        _accelAmount = _chargeData.AccelAmount;
        _decelAmount = _chargeData.DecelAmount;
        _startCharge = true;
    }

    private void OnChargeTimerEnd()
    {
        Debug.Log("Charge Ended");
        _chargeTimer.ResetTimer();
        _maxSpeed = _moveData.MaxSpeed;
        _accelAmount = _moveData.AccelAmount;
        _decelAmount = _moveData.DecelAmount;
        _startPause = false;
        _startCharge = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !_startCharge)
        {
            Direction *= -1f;
            Flip(Direction);
        }
        else if (_startCharge && !collision.gameObject.CompareTag("Player"))
        {
            _chargeTimer.EndTimer();
            _rb.velocity = Vector3.zero;
            Vector2 knockback = new Vector2(_selfKnockBack.x * Direction * -1f, _selfKnockBack.y);
            Debug.Log(knockback);
            _rb.AddForce(knockback);
            _ai.SeePlayer = false;

            //make enemy wait a bit before continuing
        }
    }

    protected override void Flip(float dir)
    {
        if (dir > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
}
