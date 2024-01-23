using Atomic.Elements;
using UnityEngine;

public class RotationMechanics
{
    private readonly IAtomicVariable<Vector3> _direction;
    private readonly Transform _transform;
    private readonly IAtomicValue<float> _speed;

    public RotationMechanics(IAtomicVariable<Vector3> direction, Transform transform, IAtomicValue<float> speed)
    {
        _direction = direction;
        _transform = transform;
        _speed = speed;
    }

    public void Update()
    {
        float targetAngle = Mathf.Atan2(_direction.Value.x, _direction.Value.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _speed.Value);
    }
}
