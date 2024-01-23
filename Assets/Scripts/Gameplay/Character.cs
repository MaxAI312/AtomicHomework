using Atomic.Elements;
using Game.Engine;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform _movementTransform;
    
    public AtomicVariable<Vector3> MovementDirection;
    public AtomicValue<float> _movementSpeed;
    
    public AtomicValue<float> _rotationSpeed;
    
    private AtomicVariable<int> _charges;
    private AtomicExpression<bool> _shootCondition;
    private AtomicAction _shootAction;
    private AtomicEvent _shootEvent;
    public AtomicVariable<bool> _enabledShooting = new(true);

    private MovementMechanics _movementMechanics;
    private RotationMechanics _rotationMechanics;

    private FireAction _fireAction;

    private void Awake()
    {
        _movementMechanics = new MovementMechanics(MovementDirection, _movementSpeed, _movementTransform);
        _rotationMechanics = new RotationMechanics(MovementDirection, _movementTransform, _rotationSpeed);
        
        _shootCondition.Append(_charges.AsFunction(it => it.Value > 0));
        _shootCondition.Append(_enabledShooting);

        _fireAction = new FireAction();
        //_fireAction.Compose();
    }

    private void Update()
    {
        _movementMechanics.Update();
        _rotationMechanics.Update();
    }
}
