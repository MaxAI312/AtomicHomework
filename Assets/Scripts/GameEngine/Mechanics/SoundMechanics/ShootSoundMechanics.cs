using Atomic.Elements;
using UnityEngine;

public class ShootSoundMechanics
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip _shootSound;
    private readonly IAtomicObservable _observable;

    public ShootSoundMechanics(
        AudioSource audioSource,
        AudioClip shootSound,
        IAtomicObservable observable)
    {
        _audioSource = audioSource;
        _shootSound = shootSound;
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
        _audioSource.clip = _shootSound;
        _audioSource.Play();
    }
}