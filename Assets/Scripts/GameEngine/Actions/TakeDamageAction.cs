using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

[Serializable]
public sealed class TakeDamageAction : IAtomicAction<int>
{
    private IAtomicVariable<int> _hitPoints;
    
    public void Compose(IAtomicVariable<int> hitPoints)
    {
        _hitPoints = hitPoints;
    }

    [Button]
    public void Invoke(int damage)
    {
        if (_hitPoints.Value <= 0) return;

        _hitPoints.Value = Math.Max(0, _hitPoints.Value - Math.Abs(damage));
    }
}