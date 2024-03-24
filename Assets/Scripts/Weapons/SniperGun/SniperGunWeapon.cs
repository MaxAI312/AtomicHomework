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
        Core.Compose(Config);
        View.Compose(Core);
    }

    private void Start()
    {
        AddData(AttackAPI.FireAction, Core.FireRaycastComponent.RangeFireAction);
    }

    private void OnDrawGizmos()
    {
        Core.FireRaycastComponent.OnDrawGizmos();
    }
}

[Serializable]
public sealed class SniperGunWeapon_Core
{
    [FormerlySerializedAs("FireComponent")] 
    public FireRaycastComponent FireRaycastComponent;

    public void Compose(WeaponConfig config)
    {
        Debug.Log(FireRaycastComponent + " - FireRaycastComponent");
        FireRaycastComponent.Compose(config);
    }
}

[Serializable]
public sealed class SniperGunWeapon_View
{
    public void Compose(SniperGunWeapon_Core core)
    {
        
    }
}
