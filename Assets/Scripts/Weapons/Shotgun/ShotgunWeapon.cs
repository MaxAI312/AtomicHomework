using System;
using System.Collections;
using System.Collections.Generic;
using Homework3;
using UnityEngine;

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
        AddData(AttackAPI.FireAction, Core.FireComponent.FireAction);
    }
}

[Serializable]
public sealed class ShotgunWeapon_Core
{
    public FireComponent FireComponent;

    public void Construct(ObjectPool objectPool)
    {
        FireComponent.Construct(objectPool);
    }
    
    public void Compose()
    {
        FireComponent.Compose();
    }
}

[Serializable]
public sealed class ShotgunWeapon_View
{
    public void Compose(ShotgunWeapon_Core core)
    {
        
    }
}
