using Atomic.Elements;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public AtomicVariable<Vector3> MovementDirection;
    public AtomicValue<float> Speed = new(2f);

    public AtomicValue<float> DurationAlive = new(5f);

    public AtomicValue<int> Damage = new(1);
        
    private MovementMechanics _movementMechanics;
    private AliveMechanics _aliveMechanics;
    private CollisionMechanics _collisionMechanics;

    public void Setup(Vector3 movementDirection, LayerMask layerMask)
    {
        MovementDirection.Value = new Vector3(movementDirection.x, 0 , movementDirection.z);
        
        _movementMechanics = new MovementMechanics(MovementDirection, Speed, _transform);
        _aliveMechanics = new AliveMechanics(DurationAlive, _transform);
        _collisionMechanics = new CollisionMechanics(Damage, _transform, layerMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionMechanics.OnTriggerEnter(other);
    }

    private void Update()
    {
        _movementMechanics.Update();
        _aliveMechanics.Update(Time.deltaTime);
    }
}