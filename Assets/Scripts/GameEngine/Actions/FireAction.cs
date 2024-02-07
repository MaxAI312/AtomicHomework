using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

[Serializable]
public class FireAction : IAtomicAction
{
    private IAtomicVariable<int> _charges;
    private IAtomicValue<bool> _shootCondition;
    private IAtomicAction _shootAction;
    private IAtomicEvent _shootEvent;
    private AnimatorDispatcher _animatorDispatcher;

    public void Compose(
        IAtomicVariable<int> charges,
        IAtomicValue<bool> shootCondition,
        IAtomicAction shootAction,
        IAtomicEvent shootEvent,
        AnimatorDispatcher animatorDispatcher
        )
    {
        _charges = charges;
        _shootCondition = shootCondition;
        _shootAction = shootAction;
        _shootEvent = shootEvent;
        _animatorDispatcher = animatorDispatcher;
        
        _animatorDispatcher.OnFireEvent += HandleFireEvent;
    }
    
    [Button]
    public void Invoke()
    {
        if (_shootCondition.Value == false) return;

        _shootEvent.Invoke();
    }

    public void Dispose()
    {
        _animatorDispatcher.OnFireEvent -= HandleFireEvent;
    }

    private void HandleFireEvent()
    {
        _charges.Value--;
        _shootAction?.Invoke();
    }
}
