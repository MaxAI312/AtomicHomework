using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class DamageHitSphereAction : IAtomicAction
{
    private static readonly Collider[] _buffer = new Collider[32];

    private IAtomicFunction<IAtomicObject, bool> _dealDamageCondition;
    private IAtomicAction<IAtomicObject> _dealDamageAction;
    private IAtomicValue<Vector3> _hitPoint;
    private IAtomicValue<float> _hitRadius;

    public void Compose(
        IAtomicFunction<IAtomicObject, bool> dealDamageCondition,
        IAtomicAction<IAtomicObject> dealDamageAction,
        IAtomicValue<Vector3> hitPoint,
        IAtomicValue<float> hitRadius)
    {
        _dealDamageCondition = dealDamageCondition;
        _dealDamageAction = dealDamageAction;
        _hitPoint = hitPoint;
        _hitRadius = hitRadius;
    }

    public void Invoke()
    {
        Debug.Log(_hitPoint.Value + " Hit Point");
        Debug.Log(_hitRadius.Value + " Hit RAdius");
        Debug.Log(_buffer.Length);
        int size = Physics.OverlapSphereNonAlloc(_hitPoint.Value, _hitRadius.Value, _buffer);
        Debug.Log(size + " - size buffer");
        for (int i = 0; i < size; i++)
        {
            Collider collider = _buffer[i];
            //if (collider.TryGetComponent(out IAtomicObject target) && _dealDamageCondition.Invoke(target))
            if (collider.TryGetComponent(out AtomicObject target) && 
                _dealDamageCondition.Invoke(target))
            {
                Debug.Log("DamageHitSphereAction - Invoke");
                _dealDamageAction.Invoke(target);
            }
        }
    }
}

public class TestDamageHitSphereAction : IAtomicAction<Vector3>
{
    private static readonly Collider[] _buffer = new Collider[32];

    private IAtomicFunction<IAtomicObject, bool> _dealDamageCondition;
    private IAtomicAction<IAtomicObject> _dealDamageAction;
    private IAtomicValue<Vector3> _hitPoint;
    private IAtomicValue<float> _hitRadius;

    public void Compose(
        IAtomicFunction<IAtomicObject, bool> dealDamageCondition,
        IAtomicAction<IAtomicObject> dealDamageAction,
        IAtomicValue<Vector3> hitPoint,
        IAtomicValue<float> hitRadius)
    {
        _dealDamageCondition = dealDamageCondition;
        _dealDamageAction = dealDamageAction;
        _hitPoint = hitPoint;
        _hitRadius = hitRadius;
    }

    public void Invoke(Vector3 args)
    {
        Debug.Log(_hitPoint.Value + " Hit Point");
        Debug.Log(_hitRadius.Value + " Hit RAdius");
        Debug.Log(_buffer.Length);
        int size = Physics.OverlapSphereNonAlloc(_hitPoint.Value, _hitRadius.Value, _buffer);
        Debug.Log(size + " - size buffer");
        for (int i = 0; i < size; i++)
        {
            Collider collider = _buffer[i];
            //if (collider.TryGetComponent(out IAtomicObject target) && _dealDamageCondition.Invoke(target))
            if (collider.TryGetComponent(out AtomicObject target) && 
                _dealDamageCondition.Invoke(target))
            {
                Debug.Log("DamageHitSphereAction - Invoke");
                _dealDamageAction.Invoke(target);
            }
        }
    }
}