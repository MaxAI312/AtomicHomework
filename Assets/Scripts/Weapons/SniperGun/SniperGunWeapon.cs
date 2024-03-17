using System;
using Homework3;
using UnityEngine;

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
        AddData(AttackAPI.FireAction, Core.FireComponent.FireAction);
    }
}

[Serializable]
public sealed class SniperGunWeapon_Core
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
public sealed class SniperGunWeapon_View
{
    public void Compose(SniperGunWeapon_Core core)
    {
        
    }
}
