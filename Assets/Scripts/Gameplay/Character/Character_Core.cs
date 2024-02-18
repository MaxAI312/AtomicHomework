using System;
using Homework3;
using UnityEngine;

[Serializable]
public sealed class Character_Core : IDisposable, IDamageable
{
    public Transform Transform;
    
    public TakeDamageAction TakeDamageAction = new();

    public FireComponent FireComponent;
    public MoveComponent MoveComponent;
    public RotationComponent RotationComponent;
    public HealthComponent HealthComponent;

    public void Construct(ObjectPool objectPool)
    {
        FireComponent.Construct(objectPool);
    }

    public void Compose()
    {
        MoveComponent.Compose(Transform);
        FireComponent.Compose();
        RotationComponent.Compose(Transform);
        HealthComponent.Compose(Transform);
        
        TakeDamageAction.Compose(HealthComponent.HitPoints);
    }

    public void OnEnable()
    {
        HealthComponent.OnEnable();
    }

    public void OnDisable()
    {
        HealthComponent.OnDisable();
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