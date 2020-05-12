using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float m_StartTime;
    private bool m_IsStarted = false;
    private float m_DurationInSeconds;
    private bool m_CanElapse = false;

    private float m_Epsilon = 0.0083333333333f;

    //To start a timer without duration
    public void Start()
    {
        m_IsStarted = true;
        m_StartTime = GetCurrentTime();
        m_CanElapse = false;
        m_DurationInSeconds = -1.0f;
    }

    public void Start(float durationInSeconds)
    {
        m_IsStarted = true;
        m_StartTime = GetCurrentTime();
        m_DurationInSeconds = durationInSeconds;
        m_CanElapse = true;
    }

    public void Start(float durationInSeconds, float initialElapsedTime)
    {
        m_IsStarted = true;
        m_StartTime = GetCurrentTime() - initialElapsedTime;
        m_DurationInSeconds = durationInSeconds;
        m_CanElapse = true;
    }

    public void Stop()
    {
        m_IsStarted = false;
        m_DurationInSeconds = 0;
    }

    public bool IsElapsed()
    {
        return IsStarted() && m_CanElapse && (GetElapsedTime() + m_Epsilon >= GetDuration());
    }

    public float GetDuration()
    {
        return m_DurationInSeconds;
    }

    public float GetElapsedTime()
    {
        if(!m_IsStarted)
        {
            return 0.0f;
        }

        return (GetCurrentTime() - m_StartTime);
    }

    public virtual float GetCurrentTime()
    {
        return Time.time;
    }

    public float GetRemainingTime()
    {
        return IsElapsed() ? 0.0f : Mathf.Max(GetDuration() - GetElapsedTime(), 0.0f);
    }

    //From 0 to 1
    public float GetProgressRatio()
    {
        if(IsStarted() && m_CanElapse)
        {
            if(GetDuration() == 0.0f)
            {
                return 1.0f;
            }

            return Mathf.Clamp01(GetElapsedTime() / GetDuration());
        }

        return 0.0f;
    }

    //From 1 to 0
    public float GetInverseProgressRatio()
    {
        return 1.0f - GetProgressRatio();
    }

    public bool IsStarted()
    {
        return m_IsStarted;
    }

    public void StopIfElapsed()
    {
        if(IsElapsed())
        {
            Stop();
        }
    }
}
