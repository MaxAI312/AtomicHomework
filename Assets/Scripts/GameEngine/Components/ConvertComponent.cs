using System;
using System.Collections.Generic;
using Atomic.Elements;
using Game.Gameplay.Conveyors;
using UnityEngine;

namespace Content
{
    [Serializable]
    public sealed class ConvertComponent
    {
        public Countdown Countdown => _countdown;
        [SerializeField] private Countdown _countdown;
            
        public IAtomicValue<int> LoadCapacity => _loadCapacity;
        [SerializeField] private AtomicVariable<int> _loadCapacity = new();
        
        public IAtomicValue<int> UnloadCapacity => _unloadCapacity;
        [SerializeField] private AtomicVariable<int> _unloadCapacity = new();

        public IAtomicVariable<int> CountLoad => _countLoad;
        [SerializeField] private AtomicVariable<int> _countLoad = new();
        
        public IAtomicVariable<int> CountUnload => _countUnload;
        [SerializeField] private AtomicVariable<int> _countUnload = new();
        
        public IAtomicValue<int> IngredientCount => _ingredientCount;
        [SerializeField] private AtomicVariable<int> _ingredientCount = new();
        
        public IAtomicValue<int> ResultCount => _resultCount;
        [SerializeField] private AtomicVariable<int> _resultCount = new();

        public IAtomicValue<bool> Enabled => _enabled;
        [SerializeField] private AtomicVariable<bool> _enabled = new(true);

        public IAtomicObservable<bool> ChangeEnabledObservable => _enabled;
        public IAtomicObservable<int> ChangeCountObservable => _countLoad;

        private ConvertMechanics _convertMechanics;
        
        public void Compose(ConveyourConfig config)
        {
            _loadCapacity.Value = config.loadCapacity;
            _unloadCapacity.Value = config.unloadCapacity;
            
            _countLoad.Value = _loadCapacity.Value;
            _countUnload.Value = _unloadCapacity.Value;

            _ingredientCount.Value = config.ingredientCount;
            _resultCount.Value = config.resultCount;

            _countdown = new Countdown(config.workTime);
            
            _convertMechanics = new ConvertMechanics(
                _countdown,
                _countLoad,
                _countUnload,
                _ingredientCount,
                _resultCount,
                _enabled
                );
        }

        public void OnEnable()
        {
            _convertMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _convertMechanics.OnDisable();
        }

        public void Update(float deltaTime)
        {
            _enabled.Value = !(_countLoad.Value <= 0 || !_countdown.IsWorking);
            
            _countdown.Tick(deltaTime);
            if (Countdown.IsWorking && _enabled.Value == false)
            {
                Countdown.Stop();
                Countdown.Reset();
            }
        }

        public void Dispose()
        {
            _loadCapacity?.Dispose();
            _unloadCapacity?.Dispose();
            _countLoad?.Dispose();
            _countUnload?.Dispose();
            _ingredientCount?.Dispose();
            _resultCount?.Dispose();
        }
    }
}