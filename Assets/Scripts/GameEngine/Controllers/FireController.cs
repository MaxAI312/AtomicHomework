using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public sealed class FireController
{
    private readonly IAtomicObject _atomicObject;
    private readonly int _numberButton;

    public FireController(IAtomicObject atomicObject, int numberButton)
    {
        _atomicObject = atomicObject;
        _numberButton = numberButton;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(_numberButton) == false) return;
        
        if (_atomicObject.TryGet(MovementAPI.IsMoving, out IAtomicValue<bool> isMoving))
            if (isMoving.Value) return;
        
        if (_atomicObject.TryGet(LifeAPI.IsAlive, out IAtomicValue<bool> isAlive))
            if (isAlive.Value == false) return;

        if (_atomicObject.TryGet(AttackAPI.CurrentWeapon, out AtomicVariable<AtomicObject> currentWeapon))
        {
            if (currentWeapon.Value.TryGet(AttackAPI.FireAction, out IAtomicAction<Vector3> fireAction))
            {
                Debug.Log(currentWeapon.Value.Get(WeaponAPI.Config));
                fireAction.Invoke(Vector3.zero);
            }
        }
    }
}
