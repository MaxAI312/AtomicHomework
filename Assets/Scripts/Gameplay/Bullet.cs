using System;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public sealed class Bullet : AtomicObject, IClearable
{
    [SerializeField] private bool _setupOnAwake;
    [SerializeField] private Transform _transform;
    
    public IAtomicValue<float> DurationLife => _durationLife;
    [SerializeField] private AtomicValue<float> _durationLife;
    
    public IAtomicVariable<float> RemainingTime => _remainingTime;
    [SerializeField] private AtomicVariable<float> _remainingTime;
    
    public IAtomicValue<int> Damage => _damage;
    [SerializeField] private AtomicValue<int> _damage;
    
    public MoveComponent MoveComponent;

    private LifetimeMechanics _lifetimeMechanics;
    private CollisionMechanics _collisionMechanics;
    
    public Cooldown Cooldown;
    
    private ObjectPool _objectPool;
    
    public void Construct(ObjectPool pool)
    {
        _objectPool = pool;
    }

    public void Setup()
    {
        Compose();
        
        MoveComponent.Compose(_transform);
        
        _collisionMechanics = new CollisionMechanics(_objectPool, gameObject, _damage);
        
        Cooldown = new Cooldown(_remainingTime.Value);
        _lifetimeMechanics = new LifetimeMechanics(_transform.gameObject, Cooldown, _objectPool);
        _lifetimeMechanics.OnEnable();
    }

    private void Awake()
    {
        if (_setupOnAwake)
        {
            Setup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionMechanics.OnTriggerEnter(other);
    }
    
    private void Update()
    {
        Cooldown.Tick(Time.deltaTime);
        MoveComponent.Update();
    }
    
    private void OnDestroy()
    {
        _lifetimeMechanics.OnDisable();
    }
    
    public void Clear()
    {
        Cooldown.Reset();
    }
}