using System;
using Atomic.Elements;
using Game.Gameplay.Conveyors;
using UnityEngine;

namespace Content
{
    [Serializable]
    public sealed class ConvertComponent
    {
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

        public IAtomicEvent OnConvertedEvent => onConvertedEvent;
        [SerializeField] private AtomicEvent onConvertedEvent = new();

        private ConvertMechanics _convertMechanics;

        private Countdown _countdown;
        
        public void Compose(Countdown countdown, ConveyourConfig config)
        {
            _loadCapacity.Value = config.loadCapacity;
            _unloadCapacity.Value = config.unloadCapacity;
            
            _countLoad.Value = _loadCapacity.Value;
            _countUnload.Value = _unloadCapacity.Value;

            _ingredientCount.Value = config.ingredientCount;
            _resultCount.Value = config.resultCount;

            _countdown = countdown;
            
            _convertMechanics = new ConvertMechanics(
                _countdown,
                _countLoad,
                _countUnload,
                _ingredientCount,
                _resultCount,
                onConvertedEvent
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

        public void Update()
        {
            _enabled.Value = _countdown.IsWorking;
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