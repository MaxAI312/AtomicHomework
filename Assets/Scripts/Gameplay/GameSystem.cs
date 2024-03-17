using Atomic.Objects;
using Homework3;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private AtomicObject _atomicObject;

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
        _moveController = new MoveController(_atomicObject);
        _fireController = new FireController(_atomicObject);
        _rotateController = new RotateController(_atomicObject);

        _weaponController = new WeaponController(_atomicObject, 0);
        _switchWeaponController = new SwitchWeaponController(_atomicObject, KeyCode.T);
        
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