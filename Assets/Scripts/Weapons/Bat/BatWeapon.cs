using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using UnityEngine;

public sealed class BatWeapon : Weapon
{
    public BatWeapon_Core Core;
    public BatWeapon_View View;

    private void Awake()
    {
        
    }
}

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
    public IsAliveEnemyFunction dealDamageCondition;
}

public sealed class BatWeapon_View
{
}

