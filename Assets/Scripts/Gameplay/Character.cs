using Atomic.Elements;
using Game.Engine;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Health")] 
    public IAtomicVariable<int> HitPoints = new AtomicVariable<int>(30);
    public IAtomicVariable<bool> IsAlive = new AtomicVariable<bool>(true);

    [Header("Move")]
    public Transform MovementTransform;
    public AtomicVariable<Vector3> MovementDirection;
    public AtomicValue<float> MovementSpeed;
    public AtomicValue<bool> MoveEnabled = new(true);

    [Header("Rotate")]
    public AtomicValue<float> RotationSpeed;
    
    [Header("Fire")]
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public AtomicVariable<int> Charges = new(10);
    public FireAction FireAction;
    public FireCondition FireCondition;
    public SpawnBulletAction BulletAction = new();

    private MovementMechanics _movementMechanics;
    private RotationMechanics _rotationMechanics;
    
    private void Awake()
    {
        _movementMechanics = new MovementMechanics(MovementDirection, MovementSpeed, MovementTransform, MoveEnabled);
        _rotationMechanics = new RotationMechanics(MovementDirection, MovementTransform, RotationSpeed);
        
        FireCondition = new FireCondition();
        FireCondition.Compose(IsAlive, Charges);

        BulletAction.Compose(FirePoint, BulletPrefab);
        
        FireAction.Compose(Charges, FireCondition, BulletAction);
    }

    private void Update()
    {
        _movementMechanics.Update();
        _rotationMechanics.Update();
    }
}
