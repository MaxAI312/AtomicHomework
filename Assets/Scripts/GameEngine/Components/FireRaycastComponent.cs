using System;
using Atomic.Elements;
using UnityEngine;

[Serializable]
public sealed class FireRaycastComponent
{
    [SerializeField] public LayerMask _layerMask;
    
    public GameObject GameObject;
    public Transform FirePoint;
    public AnimatorDispatcher AnimatorDispatcher;

    public IAtomicValue<bool> FireEnabled => _fireEnabled;
    [SerializeField] private AtomicVariable<bool> _fireEnabled;

    public IAtomicEvent FireEvent => _fireEvent;
    [SerializeField] private AtomicEvent _fireEvent;

    public AtomicFunction<bool> FireCondition = new();
    public RaycastAction RaycastAction = new();
    public AtomicAction<Vector3> RangeFireAction = new();

    public void Compose(WeaponConfig config)
    {
        ComposeCondition();
        ComposeActions(config);
    }

    private void ComposeCondition()
    {
        FireCondition.Compose(() => _fireEnabled.Value && GameObject.activeSelf);
    }

    private void ComposeActions(WeaponConfig config)
    {
        RaycastAction.Compose(
            FirePoint,
            config.Damage.AsValue(),
            _layerMask);
        
        RangeFireAction.Compose(clickPoint =>
        {
            if (FireCondition.Value)
            {
                RaycastAction.Invoke();
                FireEvent?.Invoke();
            }
        });
    }

    public void OnDrawGizmos()
    {
        if (FirePoint != null)
        {
            Vector3 direction = FirePoint.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(FirePoint.transform.position, direction * 5f);
        }
    }

    public void Dispose()
    {
        _fireEvent?.Dispose();
    }
}