using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class IsAliveEnemyFunction : IAtomicFunction<Entity, bool>
{
    private IAtomicValue<TeamType> _myTeam;
    public void Compose(IAtomicValue<TeamType> myTeam)
    {
        _myTeam = myTeam;
    }
    
    public bool Invoke(Entity target)
    {
        Debug.Log("Invoke + IsAliveEnemyFunction");
        if (target == null)
        {
            return false;
        }

        IAtomicVariable<bool> isAlive = target.GetValue<IAtomicVariable<bool>>(HealthAPI.IsAlive);
        
        
        return false;
    }
}