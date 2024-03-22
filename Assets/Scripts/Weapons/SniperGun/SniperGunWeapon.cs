using System;
using Homework3;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class SniperGunWeapon : Weapon
{
    public SniperGunWeapon_Core Core;
    public SniperGunWeapon_View View;
    
    public override WeaponConfig Config => _sniperGunWeaponConfig;
    [SerializeField] private SniperGunWeaponConfig _sniperGunWeaponConfig;
    
    public override void Compose()
    {
        base.Compose();
        Core.Compose();
        View.Compose(Core);
    }

    private void Start()
    {
        AddData(AttackAPI.FireAction, Core.fireBulletComponent.RangeFireAction);
    }
}

[Serializable]
public sealed class SniperGunWeapon_Core
{
    [FormerlySerializedAs("FireComponent")] 
    public FireBulletComponent fireBulletComponent;

    public void Construct(ObjectPool objectPool)
    {
        fireBulletComponent.Construct(objectPool);
    }
    
    public void Compose()
    {
        fireBulletComponent.Compose();
    }
}

[Serializable]
public sealed class SniperGunWeapon_View
{
    public void Compose(SniperGunWeapon_Core core)
    {
        
    }
}
