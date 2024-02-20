using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum Type
    {
        Bat = 0,
        Flamethrower = 1,
        MachineGun = 2,
        Shotgun = 3,
        SniperGun = 4
    }
    public WeaponConfig WeaponConfig => _weaponConfig;
    [SerializeField] private WeaponConfig _weaponConfig;
    
    
}
