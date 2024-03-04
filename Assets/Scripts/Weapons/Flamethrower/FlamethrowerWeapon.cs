using System;
using Atomic.Objects;
using UnityEngine;

public sealed class FlamethrowerWeapon : Weapon
{
    public FlamethrowerWeapon_Core Core;
    public FlamethrowerWeapon_View View;

    public override void Compose()
    {
        base.Compose();
        Core.Compose();
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
    
    public void Compose()
    {
        FireComponent.Compose();
        Debug.Log("COMPOSE");
    }
    
    
}

[Serializable]
public sealed class FlamethrowerWeapon_View
{
    
}

