using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using UnityEngine;

public sealed class CharacterWeaponComponent : MonoBehaviour
{
    public IAtomicAction<int> SwitchWeaponAction => _switchWeaponAction;
    [SerializeField] private AtomicAction<int> _switchWeaponAction;

    public IAtomicVariable<Weapon> CurrentWeapon => _currentWeapon;
    [SerializeField] private AtomicVariable<Weapon> _currentWeapon;
    
    ////Mpublic IAtomicValue<bool> _enabled
}
