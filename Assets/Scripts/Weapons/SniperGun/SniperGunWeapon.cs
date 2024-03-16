using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGunWeapon : Weapon
{
    public override WeaponConfig Config => _sniperGunWeaponConfig;
    [SerializeField] private SniperGunWeaponConfig _sniperGunWeaponConfig;
}
