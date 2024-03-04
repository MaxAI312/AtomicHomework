using Atomic.Elements;
using UnityEngine;

public class SelectNextWeaponWhenPreviousEndedMechanics : MonoBehaviour
{
    private readonly AtomicVariable<Weapon> _currentWeapon;
    private readonly WeaponStorage _weaponStorage;
    private readonly IAtomicAction<WeaponConfig> _switchWeaponAction;

    public SelectNextWeaponWhenPreviousEndedMechanics(
        AtomicVariable<Weapon> currentWeapon,
        WeaponStorage weaponStorage,
        IAtomicAction<WeaponConfig> switchWeaponAction)
    {
        _currentWeapon = currentWeapon;
        _weaponStorage = weaponStorage;
        _switchWeaponAction = switchWeaponAction;
    }

    public void Enable()
    {
        _currentWeapon.Subscribe(OnWeaponChanged);
        OnWeaponChanged(_currentWeapon.Value);
    }

    public void Disable()
    {
        _currentWeapon.Unsubscribe(OnWeaponChanged);
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        //_switchWeaponAction.Invoke();
    }
}
