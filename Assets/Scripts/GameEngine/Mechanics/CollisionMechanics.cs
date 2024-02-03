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
        collider.GetComponent<IDamageable>()?.TakeDamage(_damage.Value);
        _objectPool.ReturnObject(_gameObject);
    }
}