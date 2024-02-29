using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private readonly float _duration;

    [SerializeField] private float _remainingTime;
        
    public event Action Ended;
        
    public bool IsWorking { get; private set; }
    public float RemainingTime => _remainingTime;
    public float Duration => _duration;

    public Countdown(float remainingTime)
    {
        _duration = remainingTime;
        _remainingTime = remainingTime;
    }

    public void Tick(float deltaTime)
    {
        if (IsWorking)
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= deltaTime;
            }
            else
            {
                IsWorking = false;
                Ended?.Invoke();
            }
        }
    }
        
    [Button]
    public void Start()
    {
        IsWorking = true;
    }

    [Button]
    public void Stop()
    {
        IsWorking = false;
    }

    [Button]
    public void Reset()
    {
        _remainingTime = _duration;
    }
}
