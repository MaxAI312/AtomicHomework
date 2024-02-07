using Atomic.Elements;
using UnityEngine;


public sealed class ShootingEffectMechanics
{
    private readonly ParticleSystem _particle;
    private readonly IAtomicObservable _observable;

    public ShootingEffectMechanics(
        ParticleSystem particle,
        IAtomicObservable observable)
    {
        _particle = particle;
        _observable = observable;
    }

    public void OnEnable()
    {
        _observable.Subscribe(HandleEvent);
    }

    public void OnDisable()
    {
        _observable.Unsubscribe(HandleEvent);
    }

    private void HandleEvent()
    {
        _particle.Play();
    }
}