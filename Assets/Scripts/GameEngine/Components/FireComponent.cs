using System;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

[Serializable]
public class FireComponent : IDisposable
{
    public AtomicVariable<bool> FireEnabled = new(true);
    public AtomicEvent FireEvent;
    
    [Header("Pool")]
    public Transform _poolContainer;
    public ObjectPoolConfig _poolConfig;
    
    public Transform FirePoint;
    public AtomicVariable<int> Charges = new(10);

    [Get(ObjectAPI.FireAction)]
    public FireAction FireAction;
    
    public FireCondition FireCondition = new();
    public SpawnBulletAction BulletAction = new();
    
    private ObjectPoolMechanics _bulletPoolMechanics;

    public void Compose()
    {
        _bulletPoolMechanics = new ObjectPoolMechanics(_poolConfig, _poolContainer);
        
        FireCondition.Compose(FireEnabled, Charges);
        BulletAction.Compose(_bulletPoolMechanics, FirePoint);
        FireAction.Compose(Charges, FireCondition, BulletAction);
    }

    public void Dispose()
    {
        Charges?.Dispose();
        FireEvent?.Dispose();
    }
}
