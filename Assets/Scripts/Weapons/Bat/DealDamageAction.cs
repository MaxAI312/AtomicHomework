using Atomic.Elements;
using UnityEngine;

public sealed class DealDamageAction : IAtomicAction<Entity>
{
    private IAtomicValue<int> _damage;
    private Entity _owner;
    private IAtomicValue<Vector3> _point;
    private IAtomicValue<Vector3> _normal;

    private IAtomicValue<Entity> _damageEvent;

    public void Compose(
        IAtomicValue<int> damage,
        Entity owner,
        IAtomicEvent<Entity> damageEvent,
        IAtomicValue<Vector3> point = null,
        IAtomicValue<Vector3> normal = null)
    {
        _damage = damage;
        _owner = owner;
        _point = point;
        _normal = normal;
    }
    
    public void Invoke(Entity target)
    {
        //var args = new TakeDamageArgs 
        
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