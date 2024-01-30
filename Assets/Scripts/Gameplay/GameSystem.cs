using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Character _character;
    
    private MoveController _moveController;
    private FireController _fireController;
    private RotateController _rotateController;

    private void Start()
    {
        _moveController = new MoveController(_character.Core.MoveComponent.MovementDirection);
        _fireController = new FireController(_character.Core.FireComponent.FireAction);
        _rotateController = new RotateController(_character.Core.RotationComponent.RotationDirection);
    }

    private void Update()
    {
        _moveController?.Update();
        _fireController?.Update();
        _rotateController?.Update();
    }
}
