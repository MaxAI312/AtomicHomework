using System;
using Atomic.Elements;
using Homework3;
using UnityEngine;

[Serializable]
public sealed class CollisionMechanics
{
    private readonly ObjectPool _objectPool;
    private readonly GameObject _gameObject;
    private readonly IAtomicValue<int> _damage;
    
    public CollisionMechanics(
        ObjectPool objectPool,
        GameObject gameObject,
        IAtomicValue<int> damage)
    {
        _objectPool = objectPool;
        _gameObject = gameObject;
        _damage = damage;
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        TakeDamageArgs args = new TakeDamageArgs(_damage.Value);
        collider.GetComponent<IDamageable>()?.TakeDamage(args);
        _objectPool.ReturnObject(_gameObject);
    }
}