using Atomic.Elements;
using UnityEngine;

public class Stone : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform _transform;
    
    public AtomicVariable<int> HitPoints = new(3);
    public AtomicVariable<bool> IsAlive = new(true);

    public TakeDamageAction _takeDamageAction;
    
    private DeathMechanics _deathMechanics;

    private void Awake()
    {
        _takeDamageAction.Compose(HitPoints);
        
        _deathMechanics = new DeathMechanics(HitPoints, IsAlive, _transform.gameObject);
    }

    private void OnEnable()
    {
        _deathMechanics.OnEnable();
    }

    private void OnDisable()
    {
        _deathMechanics.OnDisable();
    }

    public void TakeDamage(int damage)
    {
        _takeDamageAction.Invoke(damage);
    }
}
