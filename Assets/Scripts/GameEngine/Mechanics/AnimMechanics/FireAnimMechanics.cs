using Atomic.Elements;
using UnityEngine;

public sealed class FireAnimMechanics
{
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    private readonly Animator _animator;
    private readonly IAtomicObservable _shootEvent;


    public FireAnimMechanics(
        Animator animator,
        IAtomicObservable shootEvent)
    {
        _animator = animator;
        _shootEvent = shootEvent;
    }

    public void OnEnable()
    {
        _shootEvent.Subscribe(OnStartShoot);
    }

    public void OnDisable()
    {
        _shootEvent.Unsubscribe(OnStartShoot);
    }

    private void OnStartShoot()
    {
        Debug.Log("Shoot");
        _animator.SetTrigger(Shoot);
    }
}
