using Atomic.Elements;
using Homework3;
using UnityEngine;

namespace Homework3
{
    public sealed class LifetimeMechanics
    {
        private readonly IAtomicVariable<float> _remainingTime;
        private readonly GameObject _gameObject;
        private readonly ObjectPool _objectPool;

        public LifetimeMechanics(
            IAtomicVariable<float> remainingTime,
            GameObject gameObject,
            ObjectPool objectPool)
        {
            _remainingTime = remainingTime;
            _gameObject = gameObject;
            _objectPool = objectPool;
        }

        public void Update(float deltaTime)
        {
            _remainingTime.Value -= deltaTime;
            if (_remainingTime.Value <= 0 && _gameObject.activeSelf)
            {
                _objectPool.ReturnObject(_gameObject);

            }
        }
    }
}