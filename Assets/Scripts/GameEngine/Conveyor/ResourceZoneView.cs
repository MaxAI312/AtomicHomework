using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content
{
    public sealed class ResourceZoneView
    {
        private readonly List<GameObject> _loadGameObjects;
        private readonly List<GameObject> _unloadGameObjects;

        public ResourceZoneView(
            List<GameObject> loadGameObjects,
            List<GameObject> unloadGameObjects)
        {
            _loadGameObjects = loadGameObjects;
            _unloadGameObjects = unloadGameObjects;
        }

        public void OnEnable()
        {
            _unloadGameObjects.ForEach(a => a.SetActive(false));
            RemoveResource();
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
}