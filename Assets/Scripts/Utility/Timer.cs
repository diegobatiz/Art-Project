using System;

public class Timer
{
    private float _duration;
    public float RemainingSeconds { get; private set; }

    public Timer(float duration)
    {
        RemainingSeconds = duration;
        _duration = duration;
    }

    public event Action OnTimerEnd;

    public void Tick(float deltaTime)
    {
        if (RemainingSeconds <= 0f)
        {
            return;
        }

        RemainingSeconds -= deltaTime;

        CheckForTimerEnd();
    }

    private void CheckForTimerEnd()
    {
        if (RemainingSeconds > 0f)
        {
            return;
        }

        RemainingSeconds = 0f;

        OnTimerEnd?.Invoke();
    }

    public void ResetTimer()
    {
        RemainingSeconds = _duration;
    }

    public void EndTimer()
    {
        RemainingSeconds = 0f;

        OnTimerEnd?.Invoke();
    }
}
