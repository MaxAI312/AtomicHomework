using Atomic.Elements;
using UnityEngine;

public sealed class Character : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BulletConfig _config;
    [SerializeField] private Transform _startFirePoint;
    [SerializeField] private Transform _movementTransform;
    
    public AtomicVariable<int> HitPoints = new(10);
    public AtomicVariable<bool> IsAlive = new(true);
    
    public AtomicValue<float> Speed = new(1f);

    public AtomicEvent<int> TakeDamageEvent;

    private DeathMechanics _deathMechanics;
    private TakeDamageMechanics _takeDamageMechanics;
    private FireMechanics _fireMechanics;

    private void Awake()
    {
        _deathMechanics = new DeathMechanics(HitPoints, IsAlive, _movementTransform);
        _takeDamageMechanics = new TakeDamageMechanics(TakeDamageEvent, HitPoints);
        _fireMechanics = new FireMechanics(_startFirePoint, _config, _camera);
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

    private void Update()
    {
        _fireMechanics.Update();
    }
}