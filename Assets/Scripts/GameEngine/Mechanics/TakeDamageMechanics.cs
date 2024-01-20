using System;
using Atomic.Elements;

public sealed class TakeDamageMechanics
{
    private readonly IAtomicObservable<int> _takeDamageEvent;
    private readonly IAtomicVariable<int> _hitPoints;

    public TakeDamageMechanics(IAtomicObservable<int> takeDamageEvent, IAtomicVariable<int> hitPoints)
    {
        _takeDamageEvent = takeDamageEvent;
        _hitPoints = hitPoints;
    }

    public void OnEnable()
    {
        _takeDamageEvent.Subscribe(OnTakeDamage);
    }

    public void OnDisable()
    {
        _takeDamageEvent.Unsubscribe(OnTakeDamage);
    }

    private void OnTakeDamage(int damage)
    {
        if (_hitPoints.Value <= 0) return;

        _hitPoints.Value = Math.Max(0, _hitPoints.Value - Math.Abs(damage));
    }
}