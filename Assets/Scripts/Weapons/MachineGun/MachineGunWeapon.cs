using System;
using Homework3;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class MachineGunWeapon : Weapon
{
    public MachineGunWeapon_Core Core;
    public MachineGunWeapon_View View;
    
    public override WeaponConfig Config => _machineGunWeaponConfig;
    [SerializeField] private MachineGunWeaponConfig _machineGunWeaponConfig;

    public override void Construct(ObjectPool objectPool)
    {
        Debug.Log(objectPool + " - objectPool");
        Core.Construct(objectPool);
    }

    public override void Compose()
    {
        base.Compose();
        Core.Compose();
        View.Compose(Core);
    }

    private void Start()
    {
        AddData(AttackAPI.FireAction, Core.FireBulletComponent.RangeFireAction);
    }
}

[Serializable]
public sealed class MachineGunWeapon_Core
{
    [FormerlySerializedAs("fireBulletComponent")] 
    [FormerlySerializedAs("FireComponent")] 
    public FireBulletComponent FireBulletComponent;
    
    public void Construct(ObjectPool objectPool)
    {
        FireBulletComponent.Construct(objectPool);
    }

    public void Compose()
    {
        FireBulletComponent.Compose();
    }
}

[Serializable]
public class MachineGunWeapon_View
{
    public void Compose(MachineGunWeapon_Core core)
    {
    }
}
