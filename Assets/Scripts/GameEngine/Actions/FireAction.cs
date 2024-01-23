using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

public class FireAction
{
    private IAtomicVariable<int> _charges;
    private IAtomicValue<bool> _shootCondition;
    private IAtomicAction _shootAction;
    private IAtomicEvent _shootEvent;

    public void Compose(IAtomicVariable<int> charges,
        IAtomicValue<bool> shootCondition,
        IAtomicAction shootAction,
        IAtomicEvent shootEvent)
    {
        _charges = charges;
        _shootCondition = shootCondition;
        _shootAction = shootAction;
        _shootEvent = shootEvent;
    }

    [Button]
    public void Invoke()
    {
        if (_shootCondition.Value == false) return;
        
        _shootAction.Invoke();
        _charges.Value--;
        _shootEvent.Invoke();
    }
}
