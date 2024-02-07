using UnityEngine;

public class Stone : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform _transform;

    public TakeDamageAction _takeDamageAction;

    public HealthComponent HealthComponent;

    private void Awake()
    {
        HealthComponent = new HealthComponent();
        HealthComponent.Compose(_transform);
        _takeDamageAction.Compose(HealthComponent.HitPoints);
    }

    private void OnEnable()
    {
        HealthComponent.OnEnable();
    }

    private void OnDisable()
    {
        HealthComponent.OnDisable();
    }

    public void TakeDamage(int damage)
    {
        _takeDamageAction.Invoke(damage);
    }
}
