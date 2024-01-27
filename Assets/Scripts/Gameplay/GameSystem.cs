using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private AtomicObject _character;
    
    private MoveController _moveController;
    private FireController _fireController;
    private RotateController _rotateController;

    private void Start()
    {
        IAtomicVariable<Vector3> movementDirection = _character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
        IAtomicVariable<Vector3> rotateDirection = _character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
        IAtomicAction fireAction = _character.GetAction(ObjectAPI.FireAction);
        
        _moveController = new MoveController(movementDirection);
        _fireController = new FireController(fireAction);
        _rotateController = new RotateController(rotateDirection);
    }

    private void Update()
    {
        _moveController?.Update();
        _fireController.Update();
        _rotateController?.Update();
    }
}
