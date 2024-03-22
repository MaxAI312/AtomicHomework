using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Character : AtomicObject
{
    [Header("Weapon")] 
    [SerializeField] private List<AtomicObject> _weapons;

    private IAtomicValue<TeamType> _team = new AtomicValue<TeamType>(TeamType.PLAYER);

    private ObjectPool _objectPool;

    public Character_Core Core;
    public Character_View View;

    public void Construct(ObjectPool objectPool, AudioSource audioSource)
    {
        Core.Construct(objectPool);
        View.Construct(audioSource);

        _objectPool = objectPool;

        AddData(TeamAPI.Team, _team);
        
        ConstructRangeWeapons(_objectPool);

        _weapons.ForEach(a => a.Compose());
    }

    public void Start()
    {
        Core.Compose(_weapons);
        View.Compose(Core, _weapons);

        AddData(CommonAPI.MovementDirection, Core.MoveComponent.MovementDirection);
        AddData(CommonAPI.RotationDirection, Core.RotationComponent.RotationDirection);

        AddData(AttackAPI.WeaponsStorage, _weapons);
        AddData(AttackAPI.SwitchToNextWeaponAction, Core.SwitchToNextWeaponAction);
        AddData(AttackAPI.CurrentWeapon, Core.CurrentWeapon);

        AddData(LifeAPI.IsAlive, Core.HealthComponent.IsAlive);

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
    
    private void ConstructRangeWeapons(ObjectPool objectPool)
    {
        List<Weapon> listRangeWeapons = _weapons
            .OfType<Weapon>()
            .Where(w => w.Config.Type == Weapon.Type.Range).ToList();
        
        listRangeWeapons.ForEach(a => a.Construct(objectPool));
        
        // _weapons.ForEach(a =>
        // {
        //     if (a.TryGet(WeaponAPI.Config, out WeaponConfig config))
        //     {
        //         Weapon weapon = (Weapon)a;
        //         
        //         if (config.Type == Weapon.Type.Range)
        //         {
        //             weapon.Construct(objectPool);
        //         }
        //     }
        // });
    }
}

[Serializable]
public sealed class Character_Core : IDisposable, IDamageable
{
    public Transform Transform;

    public TakeDamageAction TakeDamageAction = new();
    
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
        SetDefaultWeapon(weapons);
        ComposeComponents();
        ComposeAction(weapons);
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

    private void SetDefaultWeapon(List<AtomicObject> weapons)
    {
        if (weapons.Find(a =>
            {
                if (a.TryGet(WeaponAPI.Config, out WeaponConfig config))
                    return config.Model == Weapon.Model.Default;

                return false;
            })
            is { } found)
        {
            CurrentWeapon.Value = found;
        }
    }

    private void ComposeComponents()
    {
        MoveComponent.Compose(Transform);
        RotationComponent.Compose(Transform);
        HealthComponent.Compose(Transform);
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
        });
    }

    void IDamageable.TakeDamage(TakeDamageArgs args)
    {
        TakeDamageAction.Invoke(args);
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
        SetWeapon(core, weaponsStorage);

        _moveAnimMechanics = new MoveAnimMechanics(_animator, core.MoveComponent.IsMoving);
        //_fireAnimMechanics = new FireAnimMechanics(_animator, core.FireComponent.FireEvent);

        //_shootSoundMechanics = new ShootSoundMechanics(_audioSource, _shootSound, core.FireComponent.FireEvent);
        _deathSoundMechanics = new DeathSoundMechanics(_audioSource, _deathSound, core.HealthComponent.DeathEvent);

        //_shootingEffectMechanics = new ShootingEffectMechanics(_shootParticle, core.FireComponent.FireEvent);


        core.SwitchToNextWeaponAction.Subscribe(() =>
        {
            foreach (AtomicObject weapon in weaponsStorage.FindAll(a => a.gameObject.activeSelf)) 
                weapon.gameObject.SetActive(false);

            SetWeapon(core, weaponsStorage);
        });
    }

    private void SetWeapon(Character_Core core, List<AtomicObject> weaponsStorage)
    {
        if (weaponsStorage.Find(a =>
            {
                AtomicObject currentWeapon = core.CurrentWeapon.Value;

                if (a.TryGet(WeaponAPI.Config, out WeaponConfig targetConfig) &&
                    currentWeapon.TryGet(WeaponAPI.Config, out WeaponConfig currentConfig))
                    return targetConfig == currentConfig;

                return false;
            }) is { } found)
        {
            found.gameObject.SetActive(true);
        }
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