using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public sealed class RotateController
{
    private readonly IAtomicObject _rotateable;

    public RotateController(IAtomicObject rotateable)
    {
        _rotateable = rotateable;
    }

    public void Update()
    {
        Vector3 direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            direction.z = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.z = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1f;
        }

        if (_rotateable.TryGet(CommonAPI.RotationDirection, out IAtomicVariable<Vector3> rotateDirection))
        {
            rotateDirection.Value = direction;
        }
    }
}
