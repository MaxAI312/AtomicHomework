using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using UnityEngine;

namespace Content
{
    [Serializable]
    public sealed class TransferResourcesMechanics
    {
        private readonly List<GameObject> _loadGameObjects;
        private readonly List<GameObject> _unloadGameObjects;
        private readonly IAtomicObservable<int> _changedCountObservable;
        private readonly IAtomicValue<int> _resultCount;

        public TransferResourcesMechanics(
            List<GameObject> loadGameObjects,
            List<GameObject> unloadGameObjects,
            IAtomicObservable<int> changedCountObservable,
            IAtomicValue<int> ingredientCount)
        {
            _loadGameObjects = loadGameObjects;
            _unloadGameObjects = unloadGameObjects;
            _changedCountObservable = changedCountObservable;
            _resultCount = ingredientCount;
        }

        public void OnEnable()
        {
            _unloadGameObjects.ForEach(a => a.SetActive(false));
            DisableLastMatch();

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
                GameObject loadGameObject = _loadGameObjects.LastOrDefault(a => a.activeSelf);
                if (loadGameObject is not null)
                {
                    loadGameObject.SetActive(false);
                }
                for (int i = 0; i < _resultCount.Value; i++)
                {
                    EnableFirstMatch();
                }
            }
        }

        private void DisableLastMatch()
        {
            GameObject gameObject = _loadGameObjects.LastOrDefault(a => a.activeSelf);
            if (gameObject is not null) gameObject.SetActive(false);
        }

        private void EnableFirstMatch()
        {
            GameObject unloadGameObject = _unloadGameObjects.FirstOrDefault(a => a.activeSelf == false);
            if (unloadGameObject is not null) unloadGameObject.SetActive(true);
        }
    }
}