using Atomic.Elements;
using UnityEngine;

public sealed class RotateController
{
    private readonly IAtomicVariable<Vector3> _rotateDirection;

    public RotateController(IAtomicVariable<Vector3> rotateDirection)
    {
        _rotateDirection = rotateDirection;
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
        
        _rotateDirection.Value = direction;
    }
}
