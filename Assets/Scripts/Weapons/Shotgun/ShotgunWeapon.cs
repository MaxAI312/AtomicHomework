using System;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public sealed class ShotgunWeapon : Weapon, IDisposable
{
    public override WeaponConfig Config => _shotgunWeaponConfig;
    [SerializeField] private ShotgunWeaponConfig _shotgunWeaponConfig;
    
    public ShotgunWeapon_Core Core;
    public ShotgunWeapon_View View;
    
    public override void Compose()
    {
        base.Compose();
        Core.Compose(_shotgunWeaponConfig, this);
        View.Compose(Core);
    }

    private void Start()
    {
        AddData(AttackAPI.FireAction, Core.FireAction);
    }

    private void OnDrawGizmos()
    {
        Core.OnDrawGizmos(_shotgunWeaponConfig);
    }

    public void Dispose()
    {
        //Core.Dispose();
    }
}

[Serializable]
public sealed class ShotgunWeapon_Core
{
    [SerializeField] private Transform _hitTransform;
    [SerializeField] private GameObject _gameObject;
    
    public IAtomicValue<bool> IsEnabled => _isEnabled;
    [SerializeField] private AtomicVariable<bool> _isEnabled = new(true);

    public IAtomicValue<int> Charges => _charges;
    [SerializeField] private AtomicVariable<int> _charges;
    
    public IAtomicValue<int> Damage => _damage;
    [SerializeField] private AtomicVariable<int> _damage;
    
    public IAtomicEvent FireEvent => _fireEvent;
    [SerializeField] private AtomicEvent _fireEvent;
    
    public IAtomicValue<bool> FireCondition => _fireCondition;
    [SerializeField] private AtomicFunction<bool> _fireCondition;
    
    public IAtomicEvent<IAtomicObject> DealDamageEvent => _dealDamageEvent;
    [SerializeField] private AtomicEvent<IAtomicObject> _dealDamageEvent = new();

    public IsAliveEnemyFunction DealDamageCondition = new();

    public AtomicAction<Vector3> FireAction;
    public DamageHitSphereAction HitAction = new();
    public DealDamageAction DealDamageAction = new();

    public void Construct(ObjectPool objectPool)
    {
        
    }
    
    public void Compose(ShotgunWeaponConfig config, ShotgunWeapon weapon)
    {
        ComposeData(config);
        ComposeCondition(weapon);
        ComposeActions(weapon, config);
    }

    public void ComposeData(ShotgunWeaponConfig config)
    {
        _charges.Value = config.Charges;
        _damage.Value = config.Damage;
    }

    public void ComposeCondition(ShotgunWeapon weapon)
    {
        _fireCondition.Compose(() =>
            _isEnabled.Value && _gameObject.activeSelf && _charges.Value > 0);
        DealDamageCondition.Compose(weapon.OwnerTeam);
    }

    public void ComposeActions(ShotgunWeapon weapon, ShotgunWeaponConfig config)
    {
        DealDamageAction.Compose(
            Damage,
            weapon.Owner,
            DealDamageEvent
        );
        
        HitAction.Compose(
            DealDamageCondition,
            DealDamageAction,
            _hitTransform.position.AsValue(),
            config.HitRadius.AsValue()
            // _hitTransform.AsFunction(it => it.position),
            // config.AsFunction(it => it.HitRadius) // ПОЧЕМУ с AsValue не работает
            );
        
        FireAction.Compose((clickPoint) =>
        {
            if (_fireCondition.Value)
            {
                HitAction.Invoke();
                _fireEvent?.Invoke();
            }
        });
    }

    public void OnDrawGizmos(ShotgunWeaponConfig config)
    {
        if (_hitTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_hitTransform.position, config.HitRadius);
        }
    }

    public void Dispose()
    {
        _fireEvent.Dispose();
    }
}

[Serializable]
public sealed class ShotgunWeapon_View
{
    public void Compose(ShotgunWeapon_Core core)
    {
        
    }
}
