using System;
using Atomic.Elements;

namespace Content
{
    [Serializable]
    public sealed class ConvertMechanics
    {
        private readonly Countdown _countdown;
        private readonly ResourceZone _loadZone;
        private readonly ResourceZone _unloadZone;
        private readonly IAtomicValue<int> _ingredientCount;
        private readonly IAtomicValue<int> _resultCount;
        private readonly IAtomicVariable<bool> _enabled;

        public ConvertMechanics(
            Countdown countdown,
            ResourceZone loadZone,
            ResourceZone unloadZone,
            IAtomicValue<int> ingredientCount,
            IAtomicValue<int> resultCount,
            IAtomicVariable<bool> enabled)
        {
            _countdown = countdown;
            _loadZone = loadZone;
            _unloadZone = unloadZone;
            _ingredientCount = ingredientCount;
            _resultCount = resultCount;
            _enabled = enabled;
        }

        public void OnEnable()
        {
            _countdown.Ended += HandleEnded;
            _countdown.Start();
        }

        public void OnDisable()
        {
            _countdown.Ended -= HandleEnded;
        }

        private void HandleEnded()
        {
            if (_loadZone.Current > 0)
            {
                _loadZone.Current -= _ingredientCount.Value;
                _unloadZone.Current += _resultCount.Value;

                _countdown.Reset();
                _countdown.Start();
            }
            else
            {
                _enabled.Value = false;
                _countdown.Stop();
            }
        }
    }
}