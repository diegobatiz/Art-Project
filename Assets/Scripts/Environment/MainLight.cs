using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLight : MonoBehaviour
{
    [SerializeField] private Light _mainLight;
    private Color _mainColor;
    [SerializeField] private Color _scaryColor = new Color(140, 18, 18, 255);

    private float _mainIntensity;
    [SerializeField] private float _scaryIntensity = 641;

    private bool _switchingColors;
    private bool _turningScary = true;

    [SerializeField] private float _totalTurnTime;
    private float _elapsedTime = 0f;

    private void Awake()
    {
        _mainColor = _mainLight.color;
        _mainIntensity = _mainLight.intensity;
    }

    private void Update()
    {
        if (_switchingColors)
        {
            _elapsedTime += Time.deltaTime;

            Color startColor = _turningScary ? _mainColor : _scaryColor;
            Color endColor = _turningScary ? _scaryColor : _mainColor;
            float startIntensity = _turningScary ? _mainIntensity : _scaryIntensity;
            float endIntensity = _turningScary ? _scaryIntensity : _mainIntensity;

            Color lerpedColor = Color.Lerp(startColor, endColor, _elapsedTime);
            float lerpedIntensity = Mathf.Lerp(startIntensity, endIntensity, _elapsedTime);

            _mainLight.color = lerpedColor;
            _mainLight.intensity = lerpedIntensity;

            if (_elapsedTime >= _totalTurnTime)
            {
                _elapsedTime = 0;
                _switchingColors = false;
            }
        }
    }

    public void MakeLightScary()
    {
        _switchingColors = true;
        _turningScary = true;
    }

    public void MakeLightNormal()
    {
        _switchingColors = true;
        _turningScary = false;
    }
}