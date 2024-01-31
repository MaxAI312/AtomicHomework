using System;
using Atomic.Elements;
using Homework3;
using UnityEngine;

[Serializable]
public sealed class FireComponent : IDisposable
{
    public Transform FirePoint;
    
    public IAtomicVariable<bool> FireEnabled => _fireEnabled;
    [SerializeField] private AtomicVariable<bool> _fireEnabled;
    
    public IAtomicEvent FireEvent => _fireEvent;
    [SerializeField] private AtomicEvent _fireEvent;
    
    public IAtomicVariable<int> Charges => _charges;
    [SerializeField] private AtomicVariable<int> _charges;

    [Header("Pool")]
    public Transform _poolContainer;
    public ObjectPoolConfig _poolConfig;

    public FireCondition FireCondition = new();
    
    public SpawnBulletAction BulletAction = new();
    public FireAction FireAction;
    
    private ObjectPool bulletPool;

    public void Compose()
    {
        bulletPool = new ObjectPool(_poolConfig, _poolContainer);
        
        FireCondition.Compose(FireEnabled, Charges);
        BulletAction.Compose(bulletPool, FirePoint);
        FireAction.Compose(Charges, FireCondition, BulletAction, FireEvent);
    }

    public void Dispose()
    {
        _charges?.Dispose();
        _fireEvent?.Dispose();
    }
}
