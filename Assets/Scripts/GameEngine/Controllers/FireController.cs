using Atomic.Elements;
using UnityEngine;

public sealed class FireController
{
    private IAtomicAction _fireAction;

    public FireController(IAtomicAction fireAction)
    {
        _fireAction = fireAction;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _fireAction.Invoke();
        }
    }
}
