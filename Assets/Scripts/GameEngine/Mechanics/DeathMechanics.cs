using Atomic.Elements;
using UnityEngine;

public sealed class DeathMechanics
{
    private readonly AtomicVariable<int> _hitPoints;
    private readonly IAtomicVariable<bool> _isAlive;
    private readonly GameObject _gameObject;
    private readonly IAtomicEvent _deathEvent;

    public DeathMechanics(
        AtomicVariable<int> hitPoints,
        IAtomicVariable<bool> isAlive,
        GameObject gameObject,
        IAtomicEvent deathEvent)
    {
        _hitPoints = hitPoints;
        _isAlive = isAlive;
        _gameObject = gameObject;
        _deathEvent = deathEvent;
    }

    public void OnEnable()
    {
        _hitPoints.Subscribe(OnChangedHitPoints);
    }

    public void OnDisable()
    {
        _hitPoints.Unsubscribe(OnChangedHitPoints);
    }

    private void OnChangedHitPoints(int damage)
    {
        if (_hitPoints.Value <= 0)
        {
            _isAlive.Value = false;
            _deathEvent.Invoke();
            Object.Destroy(_gameObject);
        }
    }
}