using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using UnityEngine;

namespace Content
{
    public sealed class ResourceZoneView
    {
        private readonly List<GameObject> _loadGameObjects;
        private readonly List<GameObject> _unloadGameObjects;
        private readonly IAtomicValue<int> _resultCount;

        public ResourceZoneView()
        {
        }

        public void OnEnable()
        {
            _unloadGameObjects.ForEach(a => a.SetActive(false));
            RemoveResource();
        }

        public void OnDisable()
        {
        }

        public void RemoveResource()
        {
            GameObject gameObject = _loadGameObjects.LastOrDefault(a => a.activeSelf);
            if (gameObject is not null) gameObject.SetActive(false);
        }

        public void AddResource()
        {
            GameObject unloadGameObject = _unloadGameObjects.FirstOrDefault(a => a.activeSelf == false);
            if (unloadGameObject is not null) unloadGameObject.SetActive(true);
        }
    }

    [Serializable]
    public sealed class TransferResourcesMechanics
    {
        private readonly IAtomicObservable<int> _changedCountObservable;
        private readonly IAtomicValue<int> _resultCount;
        private readonly ResourceZoneView _resourceZoneView;

        public TransferResourcesMechanics(
            IAtomicObservable<int> changedCountObservable,
            IAtomicValue<int> resultCount,
            ResourceZoneView resourceZoneView)
        {
            _changedCountObservable = changedCountObservable;
            _resultCount = resultCount;
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
            if (value >= 0)
            {
                _resourceZoneView.RemoveResource();

                for (int i = 0; i < _resultCount.Value; i++)
                    _resourceZoneView.AddResource();

                // GameObject loadGameObject = _loadGameObjects.LastOrDefault(a => a.activeSelf);
                // if (loadGameObject is not null)
                // {
                //     loadGameObject.SetActive(false);
                // }
                // for (int i = 0; i < _resultCount.Value; i++)
                // {
                //     EnableFirstMatch();
                // }
            }
        }

        // private void DisableLastMatch()
        // {
        //     GameObject gameObject = _loadGameObjects.LastOrDefault(a => a.activeSelf);
        //     if (gameObject is not null) gameObject.SetActive(false);
        // }

        // private void EnableFirstMatch()
        // {
        //     GameObject unloadGameObject = _unloadGameObjects.FirstOrDefault(a => a.activeSelf == false);
        //     if (unloadGameObject is not null) unloadGameObject.SetActive(true);
        // }
    }
}