using System;
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

        private ResourceZone _loadZone;
        private ResourceZone _unloadZone;

        public IAtomicValue<int> IngredientCount => _ingredientCount;
        [SerializeField] private AtomicVariable<int> _ingredientCount = new();
        
        public IAtomicValue<int> ResultCount => _resultCount;
        [SerializeField] private AtomicVariable<int> _resultCount = new();

        public IAtomicValue<bool> Enabled => _enabled;
        [SerializeField] private AtomicVariable<bool> _enabled = new(true);

        public IAtomicObservable<bool> ChangeEnabledObservable => _enabled;

        public IAtomicObservable<int> ChangeCountObservable => _changeCountObservable;
        [SerializeField] private AtomicEvent<int> _changeCountObservable = new();

        private ConvertMechanics _convertMechanics;

        public void Compose(ConveyourConfig config)
        {
            _loadZone = new ResourceZone(config.loadCapacity, config.loadCapacity);
            _unloadZone = new ResourceZone(0, config.unloadCapacity);

            _changeCountObservable = _loadZone.Changed;

            _ingredientCount.Value = config.ingredientCount;
            _resultCount.Value = config.resultCount;

            _countdown = new Countdown(config.workTime);
            
            _convertMechanics = new ConvertMechanics(
                _countdown,
                _loadZone,
                _unloadZone,
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
            _enabled.Value = !(_loadZone.Current <= 0 || !_countdown.IsWorking);
            
            _countdown.Tick(deltaTime);
            if (Countdown.IsWorking && _enabled.Value == false)
            {
                Countdown.Stop();
                Countdown.Reset();
            }
        }

        public void Dispose()
        {
            _enabled?.Dispose();
            _changeCountObservable?.Dispose();
        }
    }
}