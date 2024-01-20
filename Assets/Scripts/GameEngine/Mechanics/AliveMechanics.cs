using Atomic.Elements;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class AliveMechanics
{
    private float _elapsedTime;
    
    private readonly IAtomicValue<float> _durationAlive;
    private readonly Transform _transform;

    public AliveMechanics(IAtomicValue<float> durationAlive, Transform transform)
    {
        _durationAlive = durationAlive;
        _transform = transform;
    }
    
    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;
        if (_elapsedTime >= _durationAlive.Value)
        {
            Object.Destroy(_transform.gameObject);
        }
    }
}