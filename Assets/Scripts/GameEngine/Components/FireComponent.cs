using System;
using Atomic.Elements;
using Homework3;
using UnityEngine;

[Serializable]
public sealed class FireComponent : IDisposable
{
    public GameObject GameObject;
    public Transform FirePoint;
    public AnimatorDispatcher AnimatorDispatcher;

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
        FireCondition.Compose(FireEnabled, Charges, GameObject);
        BulletAction.Compose(_bulletPool, FirePoint);
        FireAction.Compose(Charges, FireCondition, BulletAction, FireEvent, AnimatorDispatcher);
    }

    public void Dispose()
    {
        _charges?.Dispose();
        _fireEvent?.Dispose();
        FireAction?.Dispose();
    }
}
