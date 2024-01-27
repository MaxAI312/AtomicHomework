using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public sealed class Bullet : AtomicObject
{
    [SerializeField] private bool _setupOnAwake;
    [SerializeField] private Transform _transform;

    [Section]
    public MoveComponent MoveComponent;

    [Header("Lifetime")] 
    public AtomicVariable<float> ElapsedTime;
    public AtomicValue<float> DurationLife;
    public IAtomicValue<bool> LifetimeEnabled;
    
    private LifetimeMechanics _lifetimeMechanics;
    private ObjectPoolMechanics _objectPoolMechanics;

    public void Setup()
    {
        Compose();
        MoveComponent.Compose(_transform);
        
        _lifetimeMechanics = new LifetimeMechanics(ElapsedTime, DurationLife, _transform.gameObject, _objectPoolMechanics);
    }

    private void Awake()
    {
        if (_setupOnAwake)
        {
            Setup();
        }
    }

    private void Update()
    {
        MoveComponent.Update();
        _lifetimeMechanics?.Update(Time.deltaTime);
    }

    public void SetupPoolMechanics(ObjectPoolMechanics poolMechanics)
    {
        _objectPoolMechanics = poolMechanics;
    }
}