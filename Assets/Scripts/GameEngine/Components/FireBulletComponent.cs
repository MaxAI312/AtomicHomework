using System;
using Atomic.Elements;
using Homework3;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public sealed class FireBulletComponent : IDisposable
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
    [FormerlySerializedAs("rangeFireAction")] 
    [FormerlySerializedAs("FireAction")] 
    public RangeFireAction RangeFireAction;

    private ObjectPool _bulletPool;
    
    public void Construct(ObjectPool objectPool)
    {
        _bulletPool = objectPool;
        Debug.Log(_bulletPool + " - _bulletPool");
    }
    
    public void Compose()
    {
        FireCondition.Compose(FireEnabled, Charges, GameObject);
        BulletAction.Compose(_bulletPool, FirePoint);
        RangeFireAction.Compose(Charges, FireCondition, BulletAction, FireEvent, AnimatorDispatcher);
    }

    public void Dispose()
    {
        _charges?.Dispose();
        _fireEvent?.Dispose();
        RangeFireAction?.Dispose();
    }
}