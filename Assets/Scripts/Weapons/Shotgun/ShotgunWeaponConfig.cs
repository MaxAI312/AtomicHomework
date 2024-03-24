using UnityEngine;

[CreateAssetMenu(
    fileName = "ShotgunWeaponConfig",
    menuName = "Project/Configs/Weapons/Shotgun"
)]
public class ShotgunWeaponConfig : WeaponConfig
{
    public int Charges => _charges;
    [SerializeField] private int _charges;
    
    public float HitRadius => _hitRadius;
    [SerializeField] private int _hitRadius;
}
