using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public sealed class MoveComponent
{
    public AtomicVariable<Vector3> MovementDirection;
    public AtomicValue<float> MovementSpeed;
    public AtomicValue<bool> MoveEnabled;
    public AtomicFunction<bool> IsMoving;
    
    private MovementMechanics _movementMechanics;

    public void Compose(Transform transform)
    {
        _movementMechanics = new MovementMechanics(MovementDirection, MovementSpeed, transform, MoveEnabled);
        IsMoving.Compose(() => MovementDirection.Value != Vector3.zero);
    }

    public void Update()
    {
        _movementMechanics?.Update();
    }
}