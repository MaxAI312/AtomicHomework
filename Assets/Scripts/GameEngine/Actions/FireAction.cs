using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

[Serializable]
public class FireAction : IAtomicAction
{
    private IAtomicVariable<int> _charges;
    private IAtomicValue<bool> _shootCondition;
    private IAtomicAction _shootAction;

    public void Compose(
        IAtomicVariable<int> charges,
        IAtomicValue<bool> shootCondition,
        IAtomicAction shootAction)
    {
        _charges = charges;
        _shootCondition = shootCondition;
        _shootAction = shootAction;
    }
    
    [Button]
    public void Invoke()
    {
        if (_shootCondition.Value == false) return;

        _shootAction.Invoke();
        _charges.Value--;
    }
}
