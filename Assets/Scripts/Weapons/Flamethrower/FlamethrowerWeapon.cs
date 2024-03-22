using System;
using Atomic.Objects;
using Homework3;
using UnityEngine;
using UnityEngine.Serialization;

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
        AddData(AttackAPI.FireAction, Core.FireBulletComponent.RangeFireAction);
    }
}

[Serializable]
public sealed class FlamethrowerWeapon_Core
{
    public IAtomicObject Owner;
    [FormerlySerializedAs("fireBulletComponent")]
    [FormerlySerializedAs("FireComponent")] 
    public FireBulletComponent FireBulletComponent;

    public void Construct(ObjectPool objectPool)
    {
        FireBulletComponent.Construct(objectPool);
    }
    
    public void Compose()
    {
        FireBulletComponent.Compose();
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

