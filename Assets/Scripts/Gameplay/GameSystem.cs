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

    private void Awake()
    {
        _objectPool = new ObjectPool(_poolConfig, _poolContainer);
        _character.Construct(_objectPool, _audioSource);
    }

    private void Start()
    {
        _moveController = new MoveController(_atomicObject);
        _fireController = new FireController(_atomicObject, 0);
        _rotateController = new RotateController(_atomicObject);
        _switchWeaponController = new SwitchWeaponController(_atomicObject, KeyCode.T);
    }

    private void Update()
    {
        Debug.Log("Поменять public на serializeField в оружиях и корах оружия");
        _moveController?.Update();
        _fireController?.Update();
        _rotateController?.Update();
        _switchWeaponController.Update();
    }
}