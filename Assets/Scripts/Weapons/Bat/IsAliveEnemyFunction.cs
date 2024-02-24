using System;
using Atomic.Elements;
using Atomic.Objects;

public class IsAliveEnemyFunction : IAtomicFunction<Entity, bool>
{
    public bool Invoke(Entity target)
    {
        if (target == null)
        {
            return false;
        }
        
        //target.TryGetValue()
        return false;
    }
}