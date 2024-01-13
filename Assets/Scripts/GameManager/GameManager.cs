using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private EnemySystem _enemySystem;
        [SerializeField] private Player _player;

        private List<ITickable> _tickables = new(32);
        private List<IFixedTickable> _fixedTickables = new(32);
        private InputService inputService;
        
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;//IPause
        }

        private void Awake()
        {
            inputService = new InputService();
            inputService.Setup(_player);
            _tickables.Add(inputService);
            _fixedTickables.Add(inputService);
            
            _player.Construct(inputService);
            _player.Setup();
                  
            Debug.LogError(inputService + " - InputManager");
            Debug.LogError(_tickables.Count + " - _tickables.Count");
            Debug.LogError(_fixedTickables.Count + " - _fixedTickables.Count");
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            _tickables.ForEach(a => a.Tick());
        }

        private void FixedUpdate()
        {
            _fixedTickables.ForEach(a => a.FixedTick());
        }
    }

    public interface IFixedTickable
    {
        void FixedTick();
    }
    
    public interface ITickable
    {
        void Tick();
    }
}