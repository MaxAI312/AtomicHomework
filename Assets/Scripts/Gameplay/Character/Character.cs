using Atomic.Elements;
using UnityEngine;

public sealed class Character : MonoBehaviour, IAttackable
{
    [SerializeField] private BulletConfig _config;
    [SerializeField] private Transform _startFirePoint;
    [SerializeField] private Transform _movementTransform;
    
    public AtomicVariable<int> HitPoints = new(10);
    public AtomicVariable<bool> IsAlive = new(true);
    
    public AtomicValue<float> Speed = new(1f);

    public AtomicEvent<int> TakeDamageEvent;

    private SpawnBulletAction _spawnBulletAction;

    private DeathMechanics _deathMechanics;
    private TakeDamageMechanics _takeDamageMechanics;

    private void Awake()
    {
        Debug.LogError("СКОРОСТЬ ПУЛИ РАЗНАЯ"); // ИСПРАВИТЬ
        _spawnBulletAction = new SpawnBulletAction(_config, _startFirePoint);
            
        _deathMechanics = new DeathMechanics(HitPoints, IsAlive, _movementTransform);
        _takeDamageMechanics = new TakeDamageMechanics(TakeDamageEvent, HitPoints);
    }

    private void OnEnable()
    {
        _deathMechanics.OnEnable();
        _takeDamageMechanics.OnEnable();
    }

    private void OnDisable()
    {
        _deathMechanics.OnDisable();
        _takeDamageMechanics.OnDisable();
    }

    public void Fire(Vector3 direction)
    {
        _spawnBulletAction.Invoke(direction);
    }
}