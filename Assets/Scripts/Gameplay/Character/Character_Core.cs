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
    public IAtomicVariable<Vector3> RotationDirection = new AtomicVariable<Vector3>();
    public AtomicValue<float> RotationSpeed;

    public FireComponent FireComponent;
    public MoveComponent MoveComponent;

    private RotationMechanics _rotationMechanics;
    
    public void Compose()
    {
        _rotationMechanics = new RotationMechanics(RotationDirection, _transform, RotationSpeed);
        
        MoveComponent.Compose(_transform);
        FireComponent.Compose();

    }

    public void Update()
    {
        MoveComponent.Update();
        _rotationMechanics.Update();
        FireComponent.FireEnabled.Value = !MoveComponent.IsMoving.Value;
    }

    public void Dispose()
    {
        FireComponent?.Dispose();
    }
}
