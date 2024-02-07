using Atomic.Elements;
using UnityEngine;

public class DeathSoundMechanics
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip _deathSound;
    private readonly IAtomicObservable _observable;

    public DeathSoundMechanics(
        AudioSource audioSource,
        AudioClip deathSound,
        IAtomicObservable observable)
    {
        _audioSource = audioSource;
        _deathSound = deathSound;
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
        _audioSource.clip = _deathSound;
        _audioSource.Play();
    }
}