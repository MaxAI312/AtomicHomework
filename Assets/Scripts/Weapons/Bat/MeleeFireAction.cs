using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class MeleeFireAction : IAtomicAction<Vector3>
{
    private IAtomicFunction<bool> _fireCondition;
    private DamageHitSphereAction _hitAction;
    private IAtomicEvent _fireEvent;

    public void Compose(
        IAtomicFunction<bool> fireCondition,
        DamageHitSphereAction hitAction,
        IAtomicEvent fireEvent)
    {
        _fireCondition = fireCondition;
        _hitAction = hitAction;
        _fireEvent = fireEvent;
    }

    [Button]
    public void Invoke(Vector3 clickPoint)
    {
        if (_fireCondition.Value)
        {
            _hitAction.Invoke();
            _fireEvent.Invoke();
        }
    }
}