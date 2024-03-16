using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    public override WeaponConfig Config => _shotgunWeaponConfig;
    [SerializeField] private ShotgunWeaponConfig _shotgunWeaponConfig;
}
