using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Material _playerMat;

    private Timer _invincTimer;
    private float _totalTime;
    [SerializeField] private int _flashReps;

    [SerializeField] private Color _emissiveHigh;
    [SerializeField] private Color _emissiveLow;
    [SerializeField] private Color _colorHigh;
    [SerializeField] private Color _colorLow;

    private float _timePerFlash;
    private float _halfTimePerFlash;
    private float _elapsedTime;
    private int _currentRep;
    private bool _isLerpingUp;

    [SerializeField] private bool _flash = false;

    private void Awake()
    {
        _totalTime = _playerHealth.InvincTime;
        _invincTimer = new Timer(_totalTime);
        _invincTimer.OnTimerEnd += EndFlash;

        _playerHealth.OnDamaged += StartFlash;

        _timePerFlash = _totalTime / _flashReps;
        _halfTimePerFlash = _timePerFlash / 2f;
        _elapsedTime = 0f;
        _currentRep = 0;
        _isLerpingUp = true;
    }

    private void Update()
    {
        if (!_flash)
        {
            return;
        }

        _invincTimer.Tick(Time.deltaTime);

        if (_currentRep < _flashReps)
        {
            _elapsedTime += Time.deltaTime;
            float t = Mathf.PingPong(_elapsedTime, _halfTimePerFlash) / _halfTimePerFlash;
            Color startColor = _isLerpingUp ? _colorLow : _colorHigh;
            Color endColor = _isLerpingUp ? _colorHigh : _colorLow;
            Color startEmissive = _isLerpingUp ? _emissiveLow : _emissiveHigh;
            Color endEmissive = _isLerpingUp ? _emissiveHigh : _emissiveLow;

            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            Color lerpedEmissive = Color.Lerp(startEmissive, endEmissive, t);

            _playerMat.color = lerpedColor;
            _playerMat.SetColor("_EmissionColor", lerpedEmissive);

            if (_elapsedTime >= _halfTimePerFlash)
            {
                _elapsedTime -= _halfTimePerFlash;
                _isLerpingUp = !_isLerpingUp;
                if (!_isLerpingUp)
                {
                    _currentRep++;
                }
            }
        }
    }

    private void StartFlash(float damage)
    {
        _flash = true;
        _invincTimer.ResetTimer();
    }

    private void EndFlash()
    {
        _flash = false;
        _invincTimer.ResetTimer();
        _playerMat.color = new Color(1, 1, 1, 1);
        _playerMat.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
        _currentRep = 0;
        _elapsedTime = 0f;
    }
}
