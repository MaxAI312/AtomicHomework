using Atomic.Elements;
using UnityEngine;

public sealed class RaycastAction : IAtomicAction
{
    private Transform _firePoint;
    private IAtomicValue<int> _damage;
    private LayerMask _layerMask;

    public void Compose(
        Transform firePoint,
        IAtomicValue<int> damage,
        LayerMask layerMask)
    {
        _firePoint = firePoint;
        _damage = damage;
        _layerMask = layerMask;
    }

    public void Invoke()
    {
        if (Physics.Raycast(
                _firePoint.position,
                _firePoint.forward * 5f,
                out RaycastHit hitInfo,
                100, _layerMask))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                TakeDamageArgs args = new TakeDamageArgs(_damage.Value);
                damageable.TakeDamage(args);
                Debug.Log("Work");
            }
        }
    }
}