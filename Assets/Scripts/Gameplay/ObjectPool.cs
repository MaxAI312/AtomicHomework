using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Homework3
{
    [Serializable]
    public class ObjectPool
    {
        private readonly Stack<GameObject> _pool = new();

        private ObjectPoolConfig _config;
        private Transform _container;

        public ObjectPool(ObjectPoolConfig config, Transform container)
        {
            _config = config;
            _container = container;

            Fill();
        }

        private void Fill()
        {
            for (int i = 0; i < _config.Size; i++)
            {
                GameObject obj = Object.Instantiate(_config.Prefab, _container);
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
        }

        public GameObject GetObject()
        {
            if (_pool.Count == 0)
            {
                GameObject obj = Object.Instantiate(_config.Prefab, _container);
                obj.gameObject.SetActive(false);
                //obj.GetComponent<Bullet>().RemainigTime.Value = _config.DurationLifetime;
                return obj;
            }

            return _pool.Pop();
        }

        public void ReturnObject(GameObject obj)
        {
            obj.transform.position = Vector3.zero;
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }
}