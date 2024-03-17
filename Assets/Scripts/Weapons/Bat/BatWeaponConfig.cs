using UnityEngine;

[CreateAssetMenu(
    fileName = "BatWeaponConfig",
    menuName = "Project/Configs/Weapons/Bat"
)]
public class BatWeaponConfig : WeaponConfig
{
    public float HitRadius => _hitRadius;
    [SerializeField] private float _hitRadius;
}
