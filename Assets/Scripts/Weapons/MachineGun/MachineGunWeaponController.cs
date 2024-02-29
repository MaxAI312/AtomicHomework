using Atomic.Objects;
using UnityEngine;

public sealed class MachineGunWeaponController : WeaponController
{
    private const string FIRE_EVENT = "fire";
    private const string FIRE_END = "fire_end";

    private readonly IAtomicObject _owner;
    private readonly AnimatorDispatcher _ownerAnimator;

    private readonly MachineGunWeapon _weapon;
    private readonly MachineGunWeaponConfig _config;

    public MachineGunWeaponController(
        IAtomicObject owner,
        MachineGunWeapon weapon,
        MachineGunWeaponConfig config)
    {
        _owner = owner;
        _ownerAnimator = owner.Get<AnimatorDispatcher>(CommonAPI.Dispatcher);

        _weapon = weapon;
        _config = config;
    }

    public void OnEnable()
    {
        Debug.Log("OnEnable - MachineGunWeapon");
    }

    public void OnDisable()
    {
        Debug.Log("OnDisable - MachineGunWeapon");
    }
}

public abstract class WeaponController
{
    public virtual void OnEnable()
    {
        Debug.Log("OnEnable - MachineGunWeapon");
    }

    public virtual void OnDisable()
    {
        Debug.Log("OnDisable - MachineGunWeapon");
    }
}
