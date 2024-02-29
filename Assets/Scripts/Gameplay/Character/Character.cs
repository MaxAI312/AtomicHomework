using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public sealed class Character : AtomicObject
{
    public Character_Core Core;
    public Character_View View;

    public void Construct(ObjectPool objectPool, AudioSource audioSource)
    {
        Core.Construct(objectPool);
        View.Construct(audioSource);
    }
    
    public void Start()
    {
        Core.Compose();
        View.Compose(Core);
        
        AddData(CommonAPI.MovementDirection, Core.MoveComponent.MovementDirection);
        AddData(AttackAPI.FireAction, Core.FireComponent.FireAction);
        AddData(CommonAPI.RotationDirection, Core.RotationComponent.RotationDirection);

        AddData(HealthAPI.IsAlive, Core.HealthComponent.IsAlive);
        AddData(AttackAPI.SwitchToNextWeapon, Core.SwitchToNextWeaponAction);

        Core.OnEnable();
        View.OnEnable();
    }

    private void Update()
    {
        Core.Update();
        View.Update();
    }

    private void OnDestroy()
    {
        Core.OnDisable();
        View.OnDisable();
        
        Core.Dispose();
    }
}

[Serializable]
public sealed class Character_Core : IDisposable, IDamageable
{
    public Transform Transform;
    
    public TakeDamageAction TakeDamageAction = new();

    public FireComponent FireComponent;
    public MoveComponent MoveComponent;
    public RotationComponent RotationComponent;
    public HealthComponent HealthComponent;

    [Header("Weapons")] 
    public AtomicVariable<Weapon> _currentWeapon;
    
    public AtomicEvent SwitchToNextWeaponAction;
    //public AtomicEvent SwitchToRangeWeaponAction;

    public Countdown SwitchWeaponCountdown;

    public BatWeapon BatWeapon;
    public MachineGunWeapon MachineGunWeapon;
    public ShotgunWeapon ShotgunWeapon;
    public SniperGunWeapon SniperGunWeapon;
    public FlamethrowerWeapon FlamethrowerWeapon;

    public void Construct(ObjectPool objectPool)
    {
        FireComponent.Construct(objectPool);
    }

    public void Compose()
    {
        MoveComponent.Compose(Transform);
        FireComponent.Compose();
        RotationComponent.Compose(Transform);
        HealthComponent.Compose(Transform);
        
        TakeDamageAction.Compose(HealthComponent.HitPoints);
        SwitchToNextWeaponAction.Subscribe(() =>Debug.LogError("WORRRRRRKKKKK"));
    }

    public void OnEnable()
    {
        HealthComponent.OnEnable();
    }

    public void OnDisable()
    {
        HealthComponent.OnDisable();
    }

    public void Update()
    {
        MoveComponent.Update();
        FireComponent.FireEnabled.Value = !MoveComponent.IsMoving.Value;
        RotationComponent.Update();
    }

    public void Dispose()
    {
        FireComponent?.Dispose();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageAction.Invoke(damage);
    }
}

[Serializable]
public sealed class Character_View
{
    [Header("Weapon")] 
    [SerializeField] private List<Weapon> _weapons;
    
    [Header("Animator")]
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