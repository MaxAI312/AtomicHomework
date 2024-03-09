using Atomic.Objects;
using Homework3;
using UnityEngine;

public abstract class Weapon : AtomicObject
{
    public enum Type
    {
        Range,
        Melee
    }
    
    public enum Model
    {
        Bat = 0,
        Flamethrower = 1,
        MachineGun = 2,
        Shotgun = 3,
        SniperGun = 4,
        Default = Bat
    }
    
    public WeaponConfig Config => _config;
    [SerializeField] private WeaponConfig _config;

    public void Construct(ObjectPool objectPool = null)
    {
        
    }

    public override void Compose()
    {
        AddData(WeaponAPI.Config, _config);
    }
}
