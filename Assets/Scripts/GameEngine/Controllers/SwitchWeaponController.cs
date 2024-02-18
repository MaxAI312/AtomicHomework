using Atomic.Elements;
using UnityEngine;

public sealed class SwitchWeaponController
{
    private readonly IAtomicAction<int> _switchAction;
    private readonly IAtomicValue<bool> _hasSwitchEnded;

    public SwitchWeaponController(IAtomicAction<int> switchAction, IAtomicValue<bool> hasSwitchEnded)
    {
        _switchAction = switchAction;
        _hasSwitchEnded = hasSwitchEnded;
    }

    public void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll > 0f && _hasSwitchEnded.Value)
        {
            Debug.Log("ScrollDown");
            
        }
        else if (scroll < 0f && _hasSwitchEnded.Value)
        {
            Debug.Log("ScrollUp");
        }
    }
}
