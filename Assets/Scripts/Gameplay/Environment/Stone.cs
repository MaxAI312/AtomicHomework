using Atomic.Elements;
using UnityEngine;

public class Stone : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform _transform;
    
    public AtomicVariable<int> HitPoints = new(10);
    public AtomicVariable<bool> IsAlive = new(true);
    
    public AtomicEvent<int> TakeDamageEvent;

    private DeathMechanics _deathMechanics;
    private TakeDamageMechanics _takeDamageMechanics;
    
    private void Awake()
    {
        _deathMechanics = new DeathMechanics(HitPoints, IsAlive, _transform);
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
    
    public void TakeDamage(int value)
    {
        TakeDamageEvent?.Invoke(value);
    }
}
