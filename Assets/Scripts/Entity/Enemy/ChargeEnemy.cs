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

    private EnemyAI _ai;
    private bool _startCharge = false;
    private bool _startPause = false;
    private float _chargeDirection;

    public float Direction { get; private set; } = -1f;

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
            _chargeDirection = _ai.PlayerDirection;
            //flip to player direction when sprite comes

            return 0;
        }
        else if (_startPause && !_startCharge)
        {
            _pauseTimer.Tick(Time.deltaTime);
            return 0;
        }
        else if (_startCharge)
        {
            _chargeTimer.Tick(Time.deltaTime);
            return _chargeDirection;
        }
        else
        {
            return 0;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && _startCharge == false)
        {
            //Direction *= -1f;
        }
    }

    protected override void Flip(int dir)
    {
        return ;
    }
}
