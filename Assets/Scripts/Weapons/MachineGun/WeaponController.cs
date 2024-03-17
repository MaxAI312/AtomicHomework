using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class WeaponController
{
    private readonly IAtomicObject _attackable;
    private readonly int _numberButton;

    public WeaponController(IAtomicObject attackable, int numberButton)
    {
        _attackable = attackable;
        _numberButton = numberButton;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(_numberButton) == false) return;
        
        if (_attackable.TryGet(AttackAPI.CurrentWeapon, out AtomicVariable<AtomicObject> currentWeapon))
        {
            if (currentWeapon.Value.TryGet(AttackAPI.FireAction, out IAtomicAction fireAction))
            {
                Debug.Log(currentWeapon.Value.Get(WeaponAPI.Config));
                Debug.Log("EBANA SIR");
                fireAction.Invoke();
            }
        }
    }
}