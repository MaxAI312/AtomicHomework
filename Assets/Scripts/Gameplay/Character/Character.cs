using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Character : AtomicObject
{
    [Header("Weapon")] 
    [SerializeField] private List<AtomicObject> _weapons;

    public Character_Core Core;
    public Character_View View;

    public void Construct(ObjectPool objectPool, AudioSource audioSource)
    {
        Core.Construct(objectPool);
        View.Construct(audioSource);
    }

    public void Start()
    {
        _weapons.ForEach(a => a.Compose());
        Core.Compose(_weapons);
        View.Compose(Core, _weapons);

        AddData(CommonAPI.MovementDirection, Core.MoveComponent.MovementDirection);
        AddData(CommonAPI.RotationDirection, Core.RotationComponent.RotationDirection);

        AddData(AttackAPI.WeaponsStorage, _weapons);
        AddData(AttackAPI.SwitchToNextWeaponAction, Core.SwitchToNextWeaponAction);

        AddData(HealthAPI.IsAlive, Core.HealthComponent.IsAlive);


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

    //public FireComponent FireComponent;
    public MoveComponent MoveComponent;
    public RotationComponent RotationComponent;
    public HealthComponent HealthComponent;

    [Header("Weapons")] 
    public AtomicVariable<AtomicObject> CurrentWeapon = new();
    public AtomicEvent SwitchToNextWeaponAction;

    public Countdown SwitchWeaponCountdown;

    private ObjectPool _objectPool;

    public void Construct(ObjectPool objectPool)
    {
        _objectPool = objectPool;
        //FireComponent.Construct(objectPool);
    }

    public void Compose(List<AtomicObject> weapons)
    {
        //return a.Config.Type == Weapon.Type.Default;

        if (weapons.Find(a =>
            {

                //return a.Config.Type == Weapon.Type.Default;
                if (a.TryGet(WeaponAPI.Config, out WeaponConfig config))
                {
                    
                    Debug.Log("TEsSST");
                    return config.Type == Weapon.Type.Default;
                }
                return false;
            })
            is { } found)
        {
            CurrentWeapon.Value = found;
        }

        MoveComponent.Compose(Transform);
        //FireComponent.Compose();
        RotationComponent.Compose(Transform);
        HealthComponent.Compose(Transform);

        ComposeAction(weapons);
    }

    private void ComposeAction(List<AtomicObject> weapons)
    {
        TakeDamageAction.Compose(HealthComponent.HitPoints);

        SwitchToNextWeaponAction.Subscribe(() =>
        {
            int indexCurrentWeapon = weapons.IndexOf(CurrentWeapon.Value);

            if (weapons.Count == 0)
            {
                Debug.Log("Check weapons storage!!!");
                return;
            }

            if (indexCurrentWeapon + 1 == weapons.Count)
                CurrentWeapon.Value = weapons[0];
            else
                CurrentWeapon.Value = weapons[indexCurrentWeapon + 1];

            Debug.Log("Current weapon - " + CurrentWeapon.Value);
        });
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
        //FireComponent.FireEnabled.Value = !MoveComponent.IsMoving.Value;
        RotationComponent.Update();
    }

    public void Dispose()
    {
        //FireComponent?.Dispose();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageAction.Invoke(damage);
    }
}

[Serializable]
public sealed class Character_View
{
    [Header("Animator")] [SerializeField] private Animator _animator;

    [Header("SFX")] [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _deathSound;

    [Header("VFX")] [SerializeField] private ParticleSystem _shootParticle;

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

    public void Compose(Character_Core core, List<AtomicObject> weaponsStorage)
    {
        if (weaponsStorage.Find(a =>
            {
                // Weapon.Type currentWeaponType = core.CurrentWeapon.Value.Config.Type;
                // return a.Config.Type == currentWeaponType;
                AtomicObject currentWeapon = core.CurrentWeapon.Value;

                if (a.TryGet(WeaponAPI.Config, out WeaponConfig targetConfig) &&
                    currentWeapon.TryGet(WeaponAPI.Config, out WeaponConfig currentConfig))
                {
                    return targetConfig == currentConfig;
                }

                return false;
            }) is { } found)
        {
            found.gameObject.SetActive(true);
        }

        _moveAnimMechanics = new MoveAnimMechanics(_animator, core.MoveComponent.IsMoving);
        //_fireAnimMechanics = new FireAnimMechanics(_animator, core.FireComponent.FireEvent);

        //_shootSoundMechanics = new ShootSoundMechanics(_audioSource, _shootSound, core.FireComponent.FireEvent);
        _deathSoundMechanics = new DeathSoundMechanics(_audioSource, _deathSound, core.HealthComponent.DeathEvent);

        //_shootingEffectMechanics = new ShootingEffectMechanics(_shootParticle, core.FireComponent.FireEvent);


        // core.SwitchToNextWeaponAction.Subscribe(() =>
        // {
        //     foreach (Weapon weapon in weaponsStorage.FindAll(a => a.gameObject.activeSelf)) 
        //         weapon.gameObject.SetActive(false);
        //
        //     if (weaponsStorage.Find(a =>
        //         {
        //             Weapon.Type currentWeaponType = core.CurrentWeapon.Value.Config.Type;
        //             return a.Config.Type == currentWeaponType;
        //         }) is {} found1)
        //     {
        //         found1.gameObject.SetActive(true);
        //     }
        // });
    }

    public void OnEnable()
    {
        //_fireAnimMechanics.OnEnable();

        //_shootSoundMechanics.OnEnable();
        _deathSoundMechanics.OnEnable();

        //_shootingEffectMechanics.OnEnable();
    }

    public void OnDisable()
    {
        //_fireAnimMechanics.OnDisable();

        //_shootSoundMechanics.OnDisable();
        _deathSoundMechanics.OnDisable();

        //_shootingEffectMechanics.OnDisable();
    }

    public void Update()
    {
        _moveAnimMechanics.Update();
    }
}