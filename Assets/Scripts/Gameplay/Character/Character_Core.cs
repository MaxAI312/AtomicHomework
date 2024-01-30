using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public sealed class Character_Core : IDisposable
{
    public Transform _transform;
    
    [Header("Health")] 
    public IAtomicVariable<int> HitPoints = new AtomicVariable<int>(30);
    public IAtomicVariable<bool> IsAlive = new AtomicVariable<bool>(true);

    [Header("Rotate")]


    public FireComponent FireComponent;
    public MoveComponent MoveComponent;
    public RotationComponent RotationComponent;

    
    public void Compose()
    {
        MoveComponent.Compose(_transform);
        FireComponent.Compose();
        RotationComponent.Compose(_transform);
    }

    public void Update()
    {
        MoveComponent.Update();
        FireComponent.FireEnabled.Value = !MoveComponent.IsMoving.Value;
        RotationComponent.Update();
    }

    public void Dispose()
    {
        FireComponent?.Dispose();
    }
}