using Atomic.Elements;
using UnityEngine;

public sealed class MovementMechanics
{
    private readonly IAtomicValue<Vector3> _direction;
    private readonly IAtomicValue<float> _speed;
    private readonly Transform _transform;

    public MovementMechanics(AtomicValue<Vector3> direction, AtomicValue<float> speed, Transform transform)
    {
        _direction = direction;
        _speed = speed;
        _transform = transform;
    }
    
    public void Update()
    {
        Vector3 offset = _direction.Value * (Time.deltaTime * _speed.Value);
        _transform.position += offset;
    }
}