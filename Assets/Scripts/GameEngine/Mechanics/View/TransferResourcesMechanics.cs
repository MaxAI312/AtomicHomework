using System;
using Atomic.Elements;

namespace Content
{
    [Serializable]
    public sealed class TransferResourcesMechanics
    {
        private readonly IAtomicObservable<int> _changedCountObservable;
        private readonly ResourceZoneView _resourceZoneView;

        public TransferResourcesMechanics(
            IAtomicObservable<int> changedCountObservable,
            ResourceZoneView resourceZoneView)
        {
            _changedCountObservable = changedCountObservable;
            _resourceZoneView = resourceZoneView;
        }

        public void OnEnable()
        {
            _changedCountObservable.Subscribe(OnChangedCount);
        }

        public void OnDisable()
        {
            _changedCountObservable.Unsubscribe(OnChangedCount);
        }

        private void OnChangedCount(int value)
        {
            _resourceZoneView.OnChangedCount(value);
        }
    }
}