using System;
using Atomic.Elements;
using UnityEngine;

public sealed class Character : MonoBehaviour
{
    public AtomicVariable<int> HitPoints = new(10);
    public AtomicVariable<bool> IsAlive = new(true);
    
    public IAtomicAction FireAction;

    private DeathMechanics _deathMechanics;

    private void Awake()
    {
        _deathMechanics = new DeathMechanics(HitPoints, IsAlive);
    }

    private void OnEnable()
    {
        _deathMechanics.OnEnable();
    }

    private void OnDisable()
    {
        _deathMechanics.OnDisable();
    }
}

public sealed class FireMechanics
{
    
}



public sealed class Bullet : MonoBehaviour
{
    
}

public sealed class DeathMechanics
{
    private readonly AtomicVariable<int> _hitPoints;
    private readonly AtomicVariable<bool> _isAlive;

    public DeathMechanics(AtomicVariable<int> hitPoints, AtomicVariable<bool> isAlive)
    {
        _hitPoints = hitPoints;
        _isAlive = isAlive;
    }

    public void OnEnable()
    {
        _hitPoints.Subscribe(OnHealthChanged);
    }

    public void OnDisable()
    {
        _hitPoints.Unsubscribe(OnHealthChanged);
    }

    private void OnHealthChanged(int value)
    {
        if (_hitPoints.Value <= 0) 
            _isAlive.Value = false;
    }
}