using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimer : MonoBehaviour
{
    private static DebugTimer instance = null;
    private float TotalTime;
    private bool _runTimer = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public static DebugTimer Get()
    {
        return instance;
    }

    private void Update()
    {
        if (_runTimer)
        {
            TotalTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        TotalTime = 0.0f;
        _runTimer = true;
    }

    public float StopTimer()
    {
        _runTimer = false;
        return TotalTime;
    }
}
