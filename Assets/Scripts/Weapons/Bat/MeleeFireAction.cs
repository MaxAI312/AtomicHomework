using Atomic.Elements;
using UnityEngine;

public sealed class MeleeFireAction : IAtomicAction
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

    public void Invoke()
    {
        if (_fireCondition.Value)
        {
            _hitAction.Invoke();
            _fireEvent.Invoke();
        }
    }
}