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
    private ObjectPool objectPool;

    public void Setup()
    {
        Compose();
        MoveComponent.Compose(_transform);
        
        _lifetimeMechanics = new LifetimeMechanics(ElapsedTime, DurationLife, _transform.gameObject, objectPool);
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

    public void SetupPoolMechanics(ObjectPool pool)
    {
        objectPool = pool;
    }
}