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
        if (Input.GetMouseButtonDown(_numberButton))
        {
            if (_attackable.TryGet(AttackAPI.WeaponsStorage, out List<AtomicObject> weapons))//attackable наверно лучше weapon снизу назвать
            {
                foreach (AtomicObject weapon in weapons)
                {
                    //fireAction.Invoke();
                    if (weapon.TryGet(AttackAPI.FireAction, out IAtomicAction fireAction))
                    {
                        Debug.Log("EBANA SIR");
                        fireAction.Invoke();
                    }
                }
            }
        }
    }
}