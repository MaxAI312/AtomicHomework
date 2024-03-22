using Atomic.Objects;
using UnityEngine;

public class Stone : AtomicObject, IDamageable
{
    [SerializeField] private Transform _transform;

    public TakeDamageAction _takeDamageAction = new();

    public HealthComponent HealthComponent = new();
    
    private void Start()
    {
        HealthComponent.Compose(_transform);
        HealthComponent.OnEnable();
        
        _takeDamageAction.Compose(HealthComponent.HitPoints);

        AddData(LifeAPI.IsAlive, HealthComponent.IsAlive);
        AddData(LifeAPI.TakeDamageAction, _takeDamageAction);
    }

    private void OnDestroy()
    {
        HealthComponent.OnDisable();
    }

    void IDamageable.TakeDamage(TakeDamageArgs args)
    {
        _takeDamageAction.Invoke(args);
    }
}
