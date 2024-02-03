using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] private float _remainingTime;
    
    private readonly float _duration;
    
    public bool IsWorking { get; private set; }

    public event Action Ended;

    public Cooldown(float remainingTime)
    {
        _remainingTime = remainingTime;
        _duration = remainingTime;
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
