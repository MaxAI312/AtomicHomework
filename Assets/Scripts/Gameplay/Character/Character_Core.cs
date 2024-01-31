using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public sealed class Character_Core : IDisposable, IDamageable
{
    public Transform _transform;
    
    public IAtomicVariable<int> HitPoints => _hitPoints;
    [SerializeField] private AtomicVariable<int> _hitPoints = new(30);

    public IAtomicVariable<bool> IsAlive => _isAlive;
    [SerializeField] private AtomicVariable<bool> _isAlive = new(true);

    public TakeDamageAction TakeDamageAction = new();

    private DeathMechanics _deathMechanics;

    public FireComponent FireComponent;
    public MoveComponent MoveComponent;
    public RotationComponent RotationComponent;
    
    public void Compose()
    {
        TakeDamageAction.Compose(HitPoints);

        _deathMechanics = new DeathMechanics(_hitPoints, IsAlive, _transform.gameObject);
        
        MoveComponent.Compose(_transform);
        FireComponent.Compose();
        RotationComponent.Compose(_transform);
    }

    public void Update()
    {
        MoveComponent.Update();
        FireComponent.FireEnabled.Value = !MoveComponent.IsMoving.Value;
        RotationComponent.Update();
    }

    public void Dispose()
    {
        FireComponent?.Dispose();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageAction.Invoke(damage);
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}

public sealed class TakeDamageAction : IAtomicAction<int>
{
    private IAtomicVariable<int> _hitPoints;
    

    public void Compose(IAtomicVariable<int> hitPoints)
    {
        _hitPoints = hitPoints;
    }
    
    [Button]
    public void Invoke(int damage)
    {
        if (_hitPoints.Value < 0) return;

        _hitPoints.Value = Math.Max(0, _hitPoints.Value - Math.Abs(damage));
    }
}

public sealed class DeathMechanics
{
    private readonly AtomicVariable<int> _hitPoints;
    private readonly IAtomicVariable<bool> _isAlive;
    private readonly Object _obj;

    public DeathMechanics(AtomicVariable<int> hitPoints, IAtomicVariable<bool> isAlive, Object obj)
    {
        _hitPoints = hitPoints;
        _isAlive = isAlive;
        _obj = obj;
    }

    public void OnEnable()
    {
        _hitPoints.Subscribe(OnChangedHitPoints);
    }

    public void OnDisable()
    {
        _hitPoints.Unsubscribe(OnChangedHitPoints);
    }

    private void OnChangedHitPoints(int damage)
    {
        if (_hitPoints.Value <= 0)
        {
            _isAlive.Value = false;
            Object.Destroy(_obj);
        }
    }
}