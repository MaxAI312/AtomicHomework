using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class IsAliveEnemyFunction : IAtomicFunction<IAtomicObject, bool>
{
    private IAtomicValue<TeamType> _myTeam;
    public void Compose(IAtomicValue<TeamType> myTeam)
    {
        _myTeam = myTeam;
    }
    
    // public bool Invoke(AtomicObject target)
    // {
    //     Debug.Log("Invoke + IsAliveEnemyFunction");
    //     if (target == null)
    //     {
    //         return false;
    //     }
    //
    //     //IAtomicVariable<bool> isAlive = target.GetValue<IAtomicVariable<bool>>(HealthAPI.IsAlive);
    //     IAtomicVariable<bool> isAlive = target.TryGet<IAtomicVariable<bool>>(HealthAPI.IsAlive);
    //     
    //     
    //     return isAlive.Value;
    // }

    public bool Invoke(IAtomicObject target)
    {
        Debug.Log("Invoke + IsAliveEnemyFunction");
        if (target == null)
        {
            return false;
        }

        //IAtomicVariable<bool> isAlive = target.GetValue<IAtomicVariable<bool>>(HealthAPI.IsAlive);
        
        return target.TryGet(HealthAPI.IsAlive, out IAtomicValue<bool> isAlive) && isAlive.Value;
    }
}