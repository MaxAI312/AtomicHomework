using System;
using Homework3;
using UnityEngine;

public sealed class MachineGunWeapon : Weapon
{
    public MachineGunWeapon_Core Core;
    public MachineGunWeapon_View View;
    
    public override WeaponConfig Config => _machineGunWeaponConfig;
    [SerializeField] private MachineGunWeaponConfig _machineGunWeaponConfig;
    
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
public sealed class MachineGunWeapon_Core
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
public class MachineGunWeapon_View
{
    public void Compose(MachineGunWeapon_Core core)
    {
    }
}
