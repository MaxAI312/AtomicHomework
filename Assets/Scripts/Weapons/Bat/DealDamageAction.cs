using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class DealDamageAction : IAtomicAction<IAtomicObject>
{
    private IAtomicValue<int> _damage;
    private IAtomicValue<IAtomicObject> _owner;
    private IAtomicValue<Vector3> _point;
    private IAtomicValue<Vector3> _normal;

    private IAtomicEvent<IAtomicObject> _damageEvent;

    public void Compose(
        IAtomicValue<int> damage,
        IAtomicValue<IAtomicObject> owner,
        IAtomicEvent<IAtomicObject> damageEvent,
        IAtomicValue<Vector3> point = null,
        IAtomicValue<Vector3> normal = null)
    {
        _damage = damage;
        _owner = owner;
        _damageEvent = damageEvent;
        _point = point;
        _normal = normal;
    }
    
    [Button]
    public void Invoke(IAtomicObject target)
    {
        var args = new TakeDamageArgs(
            _damage.Value,
            _owner?.Value
        );
        target.InvokeAction(LifeAPI.TakeDamageAction, args);
    }
}