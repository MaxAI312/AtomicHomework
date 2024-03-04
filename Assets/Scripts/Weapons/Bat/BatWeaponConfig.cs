using UnityEngine;

[CreateAssetMenu(
    fileName = "BatWeaponConfig",
    menuName = "Project/Configs/Weapons/Bat"
)]
public class BatWeaponConfig : WeaponConfig
{
    public float HitRadius { get; set; }
}
