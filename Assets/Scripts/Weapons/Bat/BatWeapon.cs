using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public sealed class BatWeapon : Weapon
{
    public override WeaponConfig Config => _batWeaponConfig;
    [SerializeField] private BatWeaponConfig _batWeaponConfig;
    
    public BatWeapon_Core Core;
    public BatWeapon_View View;

    public override void Compose()
    {
        base.Compose();
        Core.Compose(this, _batWeaponConfig);
    }

    private void Start()
    {
        AddData(AttackAPI.FireAction, Core.FireAction);
    }
}

[Serializable]
public sealed class BatWeapon_Core
{
    //public FireComponent FireComponent;
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

    public void Compose(BatWeapon weapon, BatWeaponConfig config)
    {
        Debug.Log(weapon + " - weapon");
        //ComposeConditions(weapon);
        //ComposeActions(config, weapon);
    }

    private void ComposeConditions(BatWeapon batWeapon)
    {
        Debug.Log(batWeapon.OwnerTeam.Value + " - batWeapon.OwnerTeam");
        //DealDamageCondition.Compose(batWeapon.OwnerTeam);
    }
    
    private void ComposeActions(BatWeaponConfig config, BatWeapon weapon)
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

        DealDamageAction.Compose(
            Damage,
            weapon.Owner,
            DealDamageEvent,
            null,
            null);
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

