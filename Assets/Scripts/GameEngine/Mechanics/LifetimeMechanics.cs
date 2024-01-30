using Atomic.Elements;
using Homework3;
using UnityEngine;

namespace Homework3
{
    public sealed class LifetimeMechanics
    {
        private readonly IAtomicVariable<float> _elapsedTime;
        private readonly IAtomicValue<float> _duration;
        private readonly GameObject _gameObject;
        private readonly ObjectPool _objectPool;

        public LifetimeMechanics(
            IAtomicVariable<float> elapsedTime,
            IAtomicValue<float> duration,
            GameObject gameObject,
            ObjectPool objectPool)
        {
            _elapsedTime = elapsedTime;
            _duration = duration;
            _gameObject = gameObject;
            _objectPool = objectPool;
        }

        public void Update(float deltaTime)
        {
            _elapsedTime.Value += deltaTime;
            if (_duration.Value < _elapsedTime.Value && _gameObject.activeSelf)
            {
                _objectPool.ReturnObject(_gameObject);
                _elapsedTime.Value = 0;
            }
        }
    }
}