using UnityEngine;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        private IHitPointsComponent _hitPointsComponent;
        private IFireMechanics _fireMechanics;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private IMoveComponent _moveComponent;

        public void Setup()
        {
            _moveComponent = new MoveComponent(_rigidbody2D, _speed);
        }

        public void Subscribe()
        {
            
        }

        public void OnEnable()
        {
            //this.character.GetComponent<HitPointsComponent>().HitPointsChanged += this.OnCharacterDeath;
        }

        public void OnDisable()
        {
            //this.character.GetComponent<HitPointsComponent>().HitPointsChanged -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => gameManager.FinishGame();

        private void FixedUpdate()
        {
            _moveComponent?.Move(_inputService.HorizontalDirection);
            

            if (_fireRequired)
            {
                _fireMechanics.Fire();
                _fireRequired = false;
            }
        }
    }
}