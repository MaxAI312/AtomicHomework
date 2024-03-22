using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class TakeDamageAction : IAtomicAction<TakeDamageArgs>
{
    private IAtomicVariable<int> _hitPoints;
    
    public void Compose(IAtomicVariable<int> hitPoints)
    {
        _hitPoints = hitPoints;
    }

    [Button]
    public void Invoke(TakeDamageArgs args)
    {
        if (_hitPoints.Value < 0) return;
        
        _hitPoints.Value = Math.Max(0, _hitPoints.Value - Math.Abs(args.Damage));
        Debug.Log("TakeDamageAction - Invoke - Stone - " + _hitPoints.Value);
    }
}