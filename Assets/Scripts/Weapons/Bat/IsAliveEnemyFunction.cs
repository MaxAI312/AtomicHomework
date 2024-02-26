using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class IsAliveEnemyFunction : IAtomicFunction<Entity, bool>
{
    public bool Invoke(Entity target)
    {
        Debug.Log("Invoke + IsAliveEnemyFunction");
        if (target == null)
        {
            return false;
        }

        IAtomicVariable<bool> isAlive = target.GetValue<IAtomicVariable<bool>>(ObjectAPI.IsAlive);
        
        
        return false;
    }
}