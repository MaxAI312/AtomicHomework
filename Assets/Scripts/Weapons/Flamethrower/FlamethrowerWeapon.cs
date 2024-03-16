using System;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public sealed class FlamethrowerWeapon : Weapon
{
    public FlamethrowerWeapon_Core Core;
    public FlamethrowerWeapon_View View;

    public override WeaponConfig Config => _flamethrowerWeaponConfig;
    [SerializeField] private FlamethrowerWeaponConfig _flamethrowerWeaponConfig;

    public override void Compose()
    {
        base.Compose();
        Core.Compose();
        View.Compose(Core);
    }

    private void Start()
    {
        AddData(AttackAPI.FireAction, Core.FireComponent.FireAction);
    }
}

[Serializable]
public sealed class FlamethrowerWeapon_Core
{
    public IAtomicObject Owner;
    public FireComponent FireComponent;

    public void Construct(ObjectPool objectPool)
    {
        FireComponent.Construct(objectPool);
    }
    
    public void Compose()
    {
        FireComponent.Compose();
        //Debug.Log("COMPOSE");
    }
    
    
}

[Serializable]
public sealed class FlamethrowerWeapon_View
{
    public void Compose(FlamethrowerWeapon_Core core)
    {
        
    }
}

