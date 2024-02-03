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

    public FireCondition FireCondition = new();

    public SpawnBulletAction BulletAction = new();
    public FireAction FireAction;

    private ObjectPool _bulletPool;
    
    public void Construct(ObjectPool objectPool)
    {
        _bulletPool = objectPool;
    }
    
    public void Compose()
    {
        FireCondition.Compose(FireEnabled, Charges);
        BulletAction.Compose(_bulletPool, FirePoint);
        FireAction.Compose(Charges, FireCondition, BulletAction, FireEvent);
    }
    
    public void Dispose()
    {
        _charges?.Dispose();
        _fireEvent?.Dispose();
    }
}
