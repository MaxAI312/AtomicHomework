using System;
using Atomic.Elements;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public AtomicVariable<Vector3> MoveDirection = new(Vector3.forward);
    public AtomicValue<float> Speed = new(5f);
    public AtomicVariable<bool> IsActive = new(true);

    private MovementMechanics _movementMechanics;

    private void Awake()
    {
        _movementMechanics = new MovementMechanics(MoveDirection, Speed, _transform, IsActive);
    }

    private void Update()
    {
        _movementMechanics.Update();
    }
}
