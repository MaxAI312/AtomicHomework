using System;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public class Character : AtomicObject
{
    [SerializeField] private Transform _transform;
    
    [Header("Health")] 
    public IAtomicVariable<int> HitPoints = new AtomicVariable<int>(30);
    public IAtomicVariable<bool> IsAlive = new AtomicVariable<bool>(true);

    [Header("Rotate")]
    [Get(ObjectAPI.RotateDirection)]
    public IAtomicVariable<Vector3> RotationDirection = new AtomicVariable<Vector3>();
    public AtomicValue<float> RotationSpeed;

    [Section]
    public FireComponent FireComponent;
    
    [Section]
    public MoveComponent MoveComponent;

    private RotationMechanics _rotationMechanics;

    private void Awake()
    {
        Compose();
        _rotationMechanics = new RotationMechanics(RotationDirection, _transform, RotationSpeed);
        
        MoveComponent.Compose(_transform);
        FireComponent.Compose();
    }

    private void Update()
    {
        MoveComponent.Update();
        _rotationMechanics.Update();
    }

    private void OnDestroy()
    {
        
    }
}