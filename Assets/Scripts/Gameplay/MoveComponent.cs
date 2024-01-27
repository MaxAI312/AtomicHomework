using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

[Serializable]
public sealed class MoveComponent
{
    [Get(ObjectAPI.MoveDirection)]
    public AtomicVariable<Vector3> MovementDirection;
    public AtomicValue<float> MovementSpeed;
    public AtomicValue<bool> MoveEnabled;
    
    private MovementMechanics _movementMechanics;

    public void Compose(Transform transform)
    {
        _movementMechanics = new MovementMechanics(MovementDirection, MovementSpeed, transform, MoveEnabled);
    }

    public void Update()
    {
        _movementMechanics.Update();
    }
}