using Atomic.Elements;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public AtomicVariable<Vector3> MovementDirection;
    public AtomicValue<float> Speed = new(2f);
    
    public AtomicVariable<float> RemainingTime = new(5);

    public AtomicValue<int> Damage = new(1);
        
    private MovementMechanics _movementMechanics;
    private LifetimeMechanics _lifetimeMechanics;
    private CollisionMechanics _collisionMechanics;

    public void Setup(Vector3 movementDirection, LayerMask layerMask)
    {
        MovementDirection.Value = new Vector3(movementDirection.x, 0 , movementDirection.z);
        
        _movementMechanics = new MovementMechanics(MovementDirection, Speed, _transform);
        _lifetimeMechanics = new LifetimeMechanics(_transform, RemainingTime);
        _collisionMechanics = new CollisionMechanics(Damage, _transform, layerMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionMechanics.OnTriggerEnter(other);
    }

    private void Update()
    {
        _movementMechanics.Update();
        _lifetimeMechanics.Update(Time.deltaTime);
    }
}