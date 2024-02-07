using System;
using UnityEngine;

[Serializable]
public sealed class Character_View
{
    [SerializeField] private Animator _animator;
    
    [Header("SFX")]
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _deathSound;

    [Header("VFX")]
    [SerializeField] private ParticleSystem _shootParticle;

    private MoveAnimMechanics _moveAnimMechanics;
    private FireAnimMechanics _fireAnimMechanics;
    
    private ShootSoundMechanics _shootSoundMechanics;
    private DeathSoundMechanics _deathSoundMechanics;

    private ShootingEffectMechanics _shootingEffectMechanics;

    private AudioSource _audioSource;
    
    public void Construct(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    public void Compose(Character_Core core)
    {
        _moveAnimMechanics = new MoveAnimMechanics(_animator, core.MoveComponent.IsMoving);
        _fireAnimMechanics = new FireAnimMechanics(_animator, core.FireComponent.FireEvent);

        _shootSoundMechanics = new ShootSoundMechanics(_audioSource, _shootSound, core.FireComponent.FireEvent);
        _deathSoundMechanics = new DeathSoundMechanics(_audioSource, _deathSound, core.HealthComponent.DeathEvent);

        _shootingEffectMechanics = new ShootingEffectMechanics(_shootParticle, core.FireComponent.FireEvent);
    }

    public void OnEnable()
    {
        _fireAnimMechanics.OnEnable();
        
        _shootSoundMechanics.OnEnable();
        _deathSoundMechanics.OnEnable();
        
        _shootingEffectMechanics.OnEnable();
    }

    public void OnDisable()
    {
        _fireAnimMechanics.OnDisable();
        
        _shootSoundMechanics.OnDisable();
        _deathSoundMechanics.OnDisable();
        
        _shootingEffectMechanics.OnDisable();
    }

    public void Update()
    {
        _moveAnimMechanics.Update();
    }
}