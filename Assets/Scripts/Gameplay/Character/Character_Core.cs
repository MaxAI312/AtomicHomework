using System;
using Atomic.Elements;
using Homework3;
using UnityEngine;

[Serializable]
public sealed class Character_Core : IDisposable, IDamageable
{
    public Transform Transform;

    public IAtomicVariable<int> HitPoints => _hitPoints;
    [SerializeField] private AtomicVariable<int> _hitPoints = new(30);

    public IAtomicVariable<bool> IsAlive => _isAlive;
    [SerializeField] private AtomicVariable<bool> _isAlive = new(true);

    public TakeDamageAction TakeDamageAction = new();

    private DeathMechanics _deathMechanics;

    public FireComponent FireComponent;
    public MoveComponent MoveComponent;
    public RotationComponent RotationComponent;

    public void Construct(ObjectPool objectPool)
    {
        FireComponent.Construct(objectPool);
    }

    public void Compose()
    {
        TakeDamageAction.Compose(HitPoints);

        _deathMechanics = new DeathMechanics(_hitPoints, IsAlive, Transform.gameObject);
        
        MoveComponent.Compose(Transform);
        FireComponent.Compose();
        RotationComponent.Compose(Transform);
    }

    public void OnEnable()
    {
        _deathMechanics.OnEnable();
    }

    public void OnDisable()
    {
        _deathMechanics.OnDisable();
    }

    public void Update()
    {
        MoveComponent.Update();
        FireComponent.FireEnabled.Value = !MoveComponent.IsMoving.Value;
        RotationComponent.Update();
    }

    public void Dispose()
    {
        FireComponent?.Dispose();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageAction.Invoke(damage);
    }
}