using UnityEngine;

public sealed class Weapon : MonoBehaviour
{
    public WeaponConfig WeaponConfig => _weaponConfig;
    [SerializeField] private WeaponConfig _weaponConfig;
    
    
}
