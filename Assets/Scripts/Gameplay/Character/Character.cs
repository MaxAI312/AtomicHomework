using Atomic.Elements;
using UnityEngine;

public sealed class Character : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] private BulletConfig _config;
    [SerializeField] private Transform _startFirePoint;
    [SerializeField] private Transform _movementTransform;
    
    public AtomicVariable<int> HitPoints = new(10);
    public AtomicVariable<bool> IsAlive = new(true);
    
    public AtomicValue<float> Speed = new(1f);

    public SpawnBulletAction _spawnBulletAction;
    public TakeDamageAction _takeDamageAction;

    private DeathMechanics _deathMechanics;

    private void Awake()
    {
        _spawnBulletAction.Compose(_config, _startFirePoint);
        _takeDamageAction.Compose(HitPoints);

        _deathMechanics = new DeathMechanics(HitPoints, IsAlive, _movementTransform);
    }

    private void OnEnable() => 
        _deathMechanics.OnEnable();

    private void OnDisable() => 
        _deathMechanics.OnDisable();

    public void Fire(Vector3 direction) => 
        _spawnBulletAction.Invoke(direction);

    public void TakeDamage(int damage) => 
        _takeDamageAction.Invoke(damage);
}