using Atomic.Elements;
using Homework3;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Entity _characterEntity;

    [SerializeField] private ObjectPoolConfig _poolConfig;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private AudioSource _audioSource;

    private ObjectPool _objectPool;

    private MoveController _moveController;
    private FireController _fireController;
    private RotateController _rotateController;
    
    private SwitchWeaponController _switchWeaponController;
    private MachineGunWeaponController _machineGunWeaponController;
    
    private void Awake()
    {
        _objectPool = new ObjectPool(_poolConfig, _poolContainer);

        _character.Construct(_objectPool, _audioSource);
    }

    private void Start()
    {
        _moveController = new MoveController(_characterEntity.GetValue<IAtomicVariable<Vector3>>("MovementDirection"));
        _fireController = new FireController(_characterEntity.GetValue<FireAction>("FireAction"));
        _rotateController = new RotateController(_characterEntity.GetValue<IAtomicVariable<Vector3>>("RotationDirection"));
        
        _switchWeaponController = new SwitchWeaponController(
            _characterEntity.GetValue<SwitchWeaponAction>("SwitchWeaponAction"),
            _characterEntity.GetValue<IAtomicValue<bool>>("HasSwitchEnded"));

        //_machineGunWeaponController = new MachineGunWeaponController()
    }

    private void Update()
    {
        _moveController?.Update();
        _fireController?.Update();
        _rotateController?.Update();
        _switchWeaponController.Update();
    }
}