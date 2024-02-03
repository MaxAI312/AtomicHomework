using UnityEngine;

namespace Homework3
{
    public sealed class LifetimeMechanics
    {
        private readonly GameObject _gameObject;
        private readonly Cooldown _cooldown;
        private readonly ObjectPool _objectPool;

        public LifetimeMechanics(
            GameObject gameObject,
            Cooldown cooldown,
            ObjectPool objectPool)
        {
            _gameObject = gameObject;
            _cooldown = cooldown;
            _objectPool = objectPool;
        }

        public void OnEnable()
        {
            _cooldown.Ended += OnEnded;
        }

        public void OnDisable()
        {
            _cooldown.Ended -= OnEnded;
        }

        private void OnEnded()
        {
            _objectPool.ReturnObject(_gameObject);
        }
    }
}