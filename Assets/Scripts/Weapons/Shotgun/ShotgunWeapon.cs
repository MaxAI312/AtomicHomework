using System;
using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using Homework3;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class ShotgunWeapon : Weapon
{
    public ShotgunWeapon_Core Core;
    public ShotgunWeapon_View View;

    public override WeaponConfig Config => _shotgunWeaponConfig;
    [SerializeField] private ShotgunWeaponConfig _shotgunWeaponConfig;
    
    public override void Compose()
    {
        base.Compose();
        Core.Compose();
        View.Compose(Core);
    }

    private void Start()
    {
        //AddData(AttackAPI.FireAction, Core.fireBulletComponent.RangeFireAction);
    }
}

[Serializable]
public sealed class ShotgunWeapon_Core
{
    public IAtomicValue<bool> IsEnabled => _isEnabled;
    [SerializeField] private AtomicVariable<bool> _isEnabled;
    
    

    public void Construct(ObjectPool objectPool)
    {
        
    }
    
    public void Compose()
    {
        
    }
}

[Serializable]
public sealed class ShotgunWeapon_View
{
    public void Compose(ShotgunWeapon_Core core)
    {
        
    }
}
