using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class DeathMechanics
{
    private readonly AtomicVariable<int> _hitPoints;
    private readonly AtomicVariable<bool> _isAlive;
    private readonly Transform _transform;

    public DeathMechanics(AtomicVariable<int> hitPoints, AtomicVariable<bool> isAlive, Transform transform)
    {
        _hitPoints = hitPoints;
        _isAlive = isAlive;
        _transform = transform;
    }

    public void OnEnable()
    {
        _hitPoints.Subscribe(OnHealthChanged);
    }

    public void OnDisable()
    {
        _hitPoints.Unsubscribe(OnHealthChanged);
    }

    [Button]
    private void OnHealthChanged(int damage)
    {
        if (_hitPoints.Value <= 0)
        {
            _isAlive.Value = false;
            Object.Destroy(_transform.gameObject);
        }
    }
}