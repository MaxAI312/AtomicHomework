using Atomic.Elements;
using Homework3;
using UnityEngine;

public sealed class LifetimeMechanics
{
    private readonly IAtomicVariable<float> _elapsedTime;
    private readonly IAtomicValue<float> _duration;
    private readonly GameObject _gameObject;
    private readonly ObjectPoolMechanics _objectPoolMechanics;

    public LifetimeMechanics(
        IAtomicVariable<float> elapsedTime,
        IAtomicValue<float> duration,
        GameObject gameObject,
        ObjectPoolMechanics objectPoolMechanics)
    {
        _elapsedTime = elapsedTime;
        _duration = duration;
        _gameObject = gameObject;
        _objectPoolMechanics = objectPoolMechanics;
    }

    public void Update(float deltaTime)
    {
        _elapsedTime.Value += deltaTime;
        if (_duration.Value < _elapsedTime.Value && _gameObject.activeSelf)
        {
            _objectPoolMechanics.ReturnObject(_gameObject);
            _elapsedTime.Value = 0;
        }
    }
}