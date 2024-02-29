using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public sealed class FireController
{
    private IAtomicObject _fireable;
    
    public FireController(IAtomicObject fireable)
    {
        _fireable = fireable;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_fireable.TryGet(AttackAPI.FireAction, out IAtomicAction fireAction))
            {
                fireAction?.Invoke();
            }
        }
    }
}
