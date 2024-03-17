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
        Debug.Log("DamageHitSphereAction");
        int size = Physics.OverlapSphereNonAlloc(_hitPoint.Value, _hitRadius.Value, _buffer);

        for (int i = 0; i < size; i++)
        {

            Collider collider = _buffer[i];
            if (collider.TryGetComponent(out IAtomicObject target) && _dealDamageCondition.Invoke(target))
            {
                Debug.Log("WOOOOOOOOORK");
                //_dealDamageAction.Invoke(target);
            }
        }
    }
}