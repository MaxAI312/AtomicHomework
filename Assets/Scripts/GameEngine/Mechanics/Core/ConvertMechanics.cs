using System;
using Atomic.Elements;

namespace Content
{
    [Serializable]
    public sealed class ConvertMechanics
    {
        private readonly Countdown _countdown;
        private readonly IAtomicVariable<int> _countLoad;
        private readonly IAtomicVariable<int> _countUnload;
        private readonly IAtomicValue<int> _ingredientCount;
        private readonly IAtomicValue<int> _resultCount;
        private readonly IAtomicVariable<bool> _enabled;

        public ConvertMechanics(
            Countdown countdown,
            IAtomicVariable<int> countLoad,
            IAtomicVariable<int> countUnload,
            IAtomicValue<int> ingredientCount,
            IAtomicValue<int> resultCount,
            IAtomicVariable<bool> enabled)
        {
            _countdown = countdown;
            _countLoad = countLoad;
            _countUnload = countUnload;
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
            if (_countLoad.Value > 0)
            {
                _countLoad.Value -= _ingredientCount.Value;
                _countUnload.Value += _resultCount.Value;

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