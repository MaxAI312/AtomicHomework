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

    private void OnDrawGizmos()
    {
        Core.OnDrawGizmos(_batWeaponConfig);
    }
}

[Serializable]
public sealed class BatWeapon_Core
{
    public AtomicVariable<bool> IsEnabled = new(true);

    [Header("Attack")] 
    public AtomicFunction<bool> FireCondition = new();
    public AtomicEvent FireEvent = new();
    public MeleeFireAction FireAction = new();

    [Header("Hit")] 
    public DamageHitSphereAction HitAction = new();
    public Transform HitTransform;

    [Header("Damage")] 
    public IsAliveEnemyFunction DealDamageCondition = new();
    public DealDamageAction DealDamageAction = new();
    public AtomicEvent<IAtomicObject> DealDamageEvent = new();
    public AtomicVariable<int> Damage;

    public void Compose(BatWeapon weapon, BatWeaponConfig config)
    {
        ComposeData(config);
        ComposeConditions(weapon);
        ComposeActions(config, weapon);
    }

    private void ComposeData(BatWeaponConfig config)
    {
        Damage.Value = config.Damage;
    }

    private void ComposeConditions(BatWeapon batWeapon)
    {
        FireCondition.Compose(() => IsEnabled.Value);
        DealDamageCondition.Compose(batWeapon.OwnerTeam);
    }

    private void ComposeActions(BatWeaponConfig config, BatWeapon weapon)
    {
        FireAction.Compose(
            FireCondition,
            HitAction,
            FireEvent);

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

    public void OnDrawGizmos(BatWeaponConfig batWeaponConfig)
    {
        if (this.HitTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.HitTransform.position, batWeaponConfig.HitRadius);
        }
    }
}

[Serializable]
public sealed class BatWeapon_View
{
}