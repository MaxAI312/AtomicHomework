
using Atomic.Elements;
using UnityEngine;


public sealed class MovementMechanics
{
    private readonly IAtomicValue<Vector3> _direction;
    private readonly IAtomicValue<float> _speed;
    private readonly Transform _transform;

    public MovementMechanics(
        IAtomicValue<Vector3> direction,
        IAtomicValue<float> speed,
        Transform transform)
    {
        _speed = speed;
        _direction = direction;
        _transform = transform;
    }

    public void Update()
    {
        Vector3 offset = _direction.Value * (_speed.Value * Time.deltaTime);
        _transform.position += offset;
    }
}
