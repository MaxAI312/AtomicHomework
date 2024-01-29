using Atomic.Elements;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class LifetimeMechanics
{
    private readonly Transform _transform;
    private readonly IAtomicVariable<float> _elapsedTime;

    public LifetimeMechanics(
        Transform transform,
        IAtomicVariable<float> elapsedTime)
    {
        _transform = transform;
        _elapsedTime = elapsedTime;
    }
    
    public void Update(float deltaTime)
    {
        _elapsedTime.Value -= deltaTime;
        if (_elapsedTime.Value <= 0) 
            Object.Destroy(_transform.gameObject);
    }
}