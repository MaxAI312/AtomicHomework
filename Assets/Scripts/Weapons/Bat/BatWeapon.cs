using System;
using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public sealed class BatWeapon : Weapon
{
    public BatWeapon_Core Core;
    public BatWeapon_View View;

    private void Awake()
    {
        
    }
}

[Serializable]
public sealed class BatWeapon_Core
{
    [Header("Attack")] 
    public AtomicFunction<bool> FireCondition;
    public AtomicEvent FireEvent;
    public AtomicAction FireAction;

    [Header("Hit")] 
    public DamageHitSphereAction HitAction;
    public Transform HitTransform;

    [Header("Damage")] 
    public IsAliveEnemyFunction DealDamageCondition;
    public DealDamageAction DealDamageAction;
    public AtomicEvent<IAtomicObject> DealDamageEvent;
    public AtomicVariable<int> Damage;

    public void ComposeActions(BatWeaponConfig config)
    {
        FireAction.Compose(() =>
        {
            if (FireCondition.Value)
            {
                HitAction.Invoke();
                FireEvent.Invoke();
            }
        });
            
        HitAction.Compose(
            DealDamageCondition,
            DealDamageAction,
            HitTransform.AsFunction(it => it.position),
            config.AsFunction(it => it.HitRadius)
            );
        
        //DealDamageAction.Compose
    }
}

[Serializable]
public sealed class BatWeapon_View
{
}

