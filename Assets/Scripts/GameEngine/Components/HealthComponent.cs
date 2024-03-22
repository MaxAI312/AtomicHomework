using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public class HealthComponent
{
    public IAtomicVariable<int> HitPoints => _hitPoints;
    [SerializeField] private AtomicVariable<int> _hitPoints = new(30);

    public IAtomicVariable<bool> IsAlive => _isAlive;
    [SerializeField] private AtomicVariable<bool> _isAlive = new(true);
    
    public IAtomicEvent DeathEvent => _deathEvent;
    [SerializeField] private AtomicEvent _deathEvent;

    private DeathMechanics _deathMechanics;

    public void Compose(Transform transform)
    {
        _deathMechanics = new DeathMechanics(_hitPoints, IsAlive, transform.gameObject, _deathEvent);
    }
    
    public void OnEnable()
    {
        _deathMechanics.OnEnable();
    }

    public void OnDisable()
    {
        _deathMechanics?.OnDisable();
    }
}