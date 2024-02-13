using System;
using Atomic.Elements;
using UnityEngine;

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
        private readonly IAtomicEvent _onConverted;

        public ConvertMechanics(
            Countdown countdown,
            IAtomicVariable<int> countLoad,
            IAtomicVariable<int> countUnload,
            IAtomicValue<int> ingredientCount,
            IAtomicValue<int> resultCount,
            IAtomicEvent onConverted)
        {
            _countdown = countdown;
            _countLoad = countLoad;
            _countUnload = countUnload;
            _ingredientCount = ingredientCount;
            _resultCount = resultCount;
            _onConverted = onConverted;
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
            Debug.Log("HandleEnded");
            _countLoad.Value -= _ingredientCount.Value;
            _countUnload.Value += _resultCount.Value;
            _onConverted?.Invoke();
            _countdown.Reset();
            _countdown.Start();
        }
    }
}