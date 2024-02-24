using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using UnityEngine;

namespace Content
{
    [Serializable]
    public sealed class ResourceZoneView
    {
        private readonly IAtomicValue<int> _ingredientCount;
        private readonly IAtomicValue<int> _resultCount;
        private readonly List<GameObject> _loadGameObjects;
        private readonly List<GameObject> _unloadGameObjects;

        public ResourceZoneView(
            IAtomicValue<int> ingredientCount,
            IAtomicValue<int> resultCount,
            List<GameObject> loadGameObjects,
            List<GameObject> unloadGameObjects)
        {
            _ingredientCount = ingredientCount;
            _resultCount = resultCount;
            _loadGameObjects = loadGameObjects;
            _unloadGameObjects = unloadGameObjects;
        }

        public void OnEnable()
        {
            _unloadGameObjects.ForEach(a => a.SetActive(false));
            RemoveResource();
        }

        private void RemoveResource()
        {
            GameObject gameObject = _loadGameObjects.LastOrDefault(a => a.activeSelf);
            if (gameObject is not null) gameObject.SetActive(false);
        }

        private void AddResource()
        {
            GameObject unloadGameObject = _unloadGameObjects.FirstOrDefault(a => a.activeSelf == false);
            if (unloadGameObject is not null) unloadGameObject.SetActive(true);
        }
        
        public void OnChangedCount(int value)
        {
            if (value >= 0)
            {
                for (int i = 0; i < _ingredientCount.Value; i++) 
                    RemoveResource();
                
                for (int i = 0; i < _resultCount.Value; i++)
                    AddResource();
            }
        }
    }
}