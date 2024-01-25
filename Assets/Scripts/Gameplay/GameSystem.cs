using System;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Character _character;

    private MoveController _moveController;
    private FireController _fireController;

    private void Awake()
    {
        _moveController = new MoveController(_character.MovementDirection);
        _fireController = new FireController(_character.FireAction);
    }

    private void Update()
    {
        _moveController.Update();
        _fireController.Update();
    }
}
