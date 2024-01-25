using Atomic.Elements;
using UnityEngine;

public sealed class MoveController
{
    private readonly IAtomicVariable<Vector3> _moveDirection;

    public MoveController(IAtomicVariable<Vector3> moveDirection)
    {
        _moveDirection = moveDirection;
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

        _moveDirection.Value = direction;
    }
}
