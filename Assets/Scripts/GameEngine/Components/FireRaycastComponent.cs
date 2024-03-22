using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public sealed class FireRaycastComponent : IDisposable
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
    public RaycastAction RaycastAction = new();
    public RangeFireAction RangeFireAction;

    public void Compose()
    {
        FireCondition.Compose(FireEnabled, Charges, GameObject);
        RaycastAction.Compose(FirePoint);
        RangeFireAction.Compose(Charges, FireCondition, RaycastAction, FireEvent, AnimatorDispatcher);
    }

    public void Dispose()
    {
        _charges?.Dispose();
        _fireEvent?.Dispose();
        RangeFireAction?.Dispose();
    }
}