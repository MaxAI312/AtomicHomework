using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public sealed class MoveComponent
{
    public IAtomicVariable<Vector3> MovementDirection => _moveDirection;
    [SerializeField] private AtomicVariable<Vector3> _moveDirection;

    public IAtomicValue<float> MovementSpeed => _movementSpeed;
    [SerializeField] private AtomicValue<float> _movementSpeed;
    
    public IAtomicValue<bool> MoveEnabled => _moveEnabled;
    [SerializeField] private AtomicValue<bool> _moveEnabled;

    public IAtomicFunction<bool> IsMoving => _isMoving;
    [SerializeField] private AtomicFunction<bool> _isMoving;

    private MovementMechanics _movementMechanics;

    public void Compose(Transform transform)
    {
        _movementMechanics = new MovementMechanics(MovementDirection, MovementSpeed, transform, MoveEnabled);
        _isMoving.Compose(() => MovementDirection.Value != Vector3.zero);
    }

    public void Update()
    {
        _movementMechanics?.Update();
    }
}