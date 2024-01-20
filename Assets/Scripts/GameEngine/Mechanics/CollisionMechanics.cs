using Atomic.Elements;
using UnityEngine;

public class CollisionMechanics
{
    private readonly IAtomicValue<int> _damage;
    private readonly Transform _transform;
    private readonly LayerMask _layerMask;
    
    public CollisionMechanics(IAtomicValue<int> damage, Transform transform, LayerMask layerMask)
    {
        _damage = damage;
        _transform = transform;
        _layerMask = layerMask;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if ((_layerMask.value & (1 << collider.gameObject.layer)) != 0)
        {
            collider.GetComponent<IDamageable>()?.TakeDamage(_damage.Value);
            Object.Destroy(_transform.gameObject);
        }
    }
}