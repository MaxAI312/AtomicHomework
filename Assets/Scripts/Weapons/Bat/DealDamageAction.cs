using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

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
    

    public void Invoke(IAtomicObject args)
    {
        
    }
}

public struct TakeDamageArg
{
    // public int Damage;
    // public Vector3? Point;
    // public Vector3? Normal;
    // public Entity Owner;
    //
    // public TakeDamageArg(
    //     int damage,
    //     Vector3? point = null,
    //     Vector3? normal = null)
    // {
    //     
    // }
}