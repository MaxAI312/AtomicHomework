using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Homework3;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Character _character;

    [SerializeField] private ObjectPoolConfig _poolConfig;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private AudioSource _audioSource;

    private ObjectPool _objectPool;

    private MoveController _moveController;
    private FireController _fireController;
    private RotateController _rotateController;
    
    private SwitchWeaponController _switchWeaponController;
    private WeaponController _weaponController;
    //private MachineGunWeaponController _machineGunWeaponController;
    
    private void Awake()
    {
        _objectPool = new ObjectPool(_poolConfig, _poolContainer);

        _character.Construct(_objectPool, _audioSource);
    }

    private void Start()
    {
        _moveController = new MoveController(_character);
        _fireController = new FireController(_character);
        _rotateController = new RotateController(_character);

        _weaponController = new WeaponController(_character, 0);
        _switchWeaponController = new SwitchWeaponController(_character, KeyCode.T);
        
         // _switchWeaponController = new SwitchWeaponController(
         //     _characterEntity.GetValue<SwitchWeaponAction>("SwitchWeaponAction"),
         //     _characterEntity.GetValue<IAtomicValue<bool>>("HasSwitchEnded"));

         //_machineGunWeaponController = new MachineGunWeaponController();
    }

    private void Update()
    {
        _moveController?.Update();
        _fireController?.Update();
        _rotateController?.Update();
        _switchWeaponController.Update();
        _weaponController.Update();
    }
}