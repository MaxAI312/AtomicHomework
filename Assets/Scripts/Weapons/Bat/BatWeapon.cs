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

    public override void Compose()
    {
        base.Compose();
        Core.Compose();
    }
}

[Serializable]
public sealed class BatWeapon_Core
{
    public IAtomicObject Owner;
    public FireComponent FireComponent;
    // [Header("Attack")] 
    // public AtomicFunction<bool> FireCondition;
    // public AtomicEvent FireEvent;
    // public AtomicAction FireAction;
    //
    // [Header("Hit")] 
    // public DamageHitSphereAction HitAction;
    // public Transform HitTransform;
    //
    // [Header("Damage")] 
    // public IsAliveEnemyFunction DealDamageCondition;
    // public DealDamageAction DealDamageAction;
    // public AtomicEvent<IAtomicObject> DealDamageEvent;
    // public AtomicVariable<int> Damage;

    public void Compose()
    {
        Debug.Log("COMPOSE");
    }

    // public void Compose(IAtomicObject owner)
    // {
    //     FireComponent.Compose();
    //     Owner = owner;
    //     Debug.Log(Owner + " Owner");
    // }
    
    private void ComposeData()
    {
        
    }

    private void ComposeActions(BatWeaponConfig config)
    {

        // FireAction.Compose(() =>
        // {
        //     if (FireCondition.Value)
        //     {
        //         HitAction.Invoke();
        //         FireEvent.Invoke();
        //     }
        // });
        //     
        // HitAction.Compose(
        //     DealDamageCondition,
        //     DealDamageAction,
        //     HitTransform.AsFunction(it => it.position),
        //     config.AsFunction(it => it.HitRadius)
        //     );
        
        //DealDamageAction.Compose
    }

    public void OnEnable()
    {
    }

    public void OnDisable()
    {
        
    }

    public void Update()
    {
        
        //FireComponent.FireEnabled.Value = 
    }
//
// #if UNITY_EDITOR
//         public void OnDrawGizmos(BatWeaponConfig config)
//         {
//             if (HitTransform != null)
//             {
//                 Gizmos.color = Color.red;
//                 Gizmos.DrawWireSphere(this.HitTransform.position, config.HitRadius);
//             }
//         }
// #endif
}

[Serializable]
public sealed class BatWeapon_View
{
}

