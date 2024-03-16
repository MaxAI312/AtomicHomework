using Atomic.Objects;
using Homework3;
using UnityEngine;

public class MachineGunWeapon : Weapon
{
    public override WeaponConfig Config => _machineGunWeaponConfig;
    [SerializeField] private MachineGunWeaponConfig _machineGunWeaponConfig;
    
    // protected override void Compose()
    // {
    //     base.Compose();
    //     Core.Compose();
    // }
}
