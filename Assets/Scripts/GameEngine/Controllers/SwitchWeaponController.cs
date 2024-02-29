using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public sealed class SwitchWeaponController
{
    private readonly IAtomicObject _character;
    private readonly KeyCode _keyCode;

    public SwitchWeaponController(
        IAtomicObject character,
        KeyCode keyCode)
    {
        _character = character;
        _keyCode = keyCode;
    }

    public void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (_character.TryGet(AttackAPI.SwitchToNextWeapon, out IAtomicEvent switchToNextWeaponAction))
        {
            if (Input.GetKeyDown(_keyCode))
            {
                switchToNextWeaponAction.Invoke();
            }
        }




        // if (scroll > 0f && _hasSwitchEnded.Value)
        // {
        //     Debug.Log("ScrollDown");
        //     
        // }
        // else if (scroll < 0f && _hasSwitchEnded.Value)
        // {
        //     Debug.Log("ScrollUp");
        // }
    }
}
