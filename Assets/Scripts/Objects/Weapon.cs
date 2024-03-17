using Atomic.Elements;
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
    
    public abstract WeaponConfig Config { get; }
    // public WeaponConfig Config => _config;
    // [SerializeField] private WeaponConfig _config;

    public ObjectPool ObjectPool => _objectPool;
    private ObjectPool _objectPool;
    
    public IAtomicValue<IAtomicObject> Owner => _owner;
    [SerializeField] private AtomicVariable<AtomicObject> _owner;

    public IAtomicValue<TeamType> OwnerTeam => _ownerTeam;
    [SerializeField] private GetTeamOwnerFunction _ownerTeam;

    public virtual void Construct(ObjectPool objectPool = null)
    {
        _objectPool = objectPool;
    }

    public override void Compose()
    {
        base.Compose();

        //AddData(TeamAPI.Team, _ownerTeam);
        AddData(WeaponAPI.Config, Config);
        _ownerTeam.Compose(_owner);
    }
}