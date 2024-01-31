using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public sealed class RotationComponent
{
    public IAtomicVariable<Vector3> RotationDirection => _rotationDirection;
    [SerializeField] private AtomicVariable<Vector3> _rotationDirection;

    public IAtomicValue<float> RotationSpeed => _rotationSpeed;
    [SerializeField] private AtomicValue<float> _rotationSpeed;

    private RotationMechanics _rotationMechanics;

    public void Compose(Transform transform)
    {
        _rotationMechanics = new RotationMechanics(RotationDirection, transform, RotationSpeed);
    }

    public void Update()
    {
        _rotationMechanics.Update();
    }
}