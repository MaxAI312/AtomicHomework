using System;

namespace ShootEmUp
{
    public interface IHitPointsComponent
    {
        event Action<int> HitPointsChanged;
        bool IsHitPointsExists();
        void TakeDamage(int damage);
    }

    public sealed class HitPointsComponent : IHitPointsComponent
    {
        private int _hitPoints;
        
        public event Action<int> HitPointsChanged;

        public bool IsHitPointsExists() {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            if (_hitPoints <= 0)
            {
                _hitPoints = Math.Max(0, _hitPoints - damage);
                HitPointsChanged?.Invoke(_hitPoints);
            }
        }
    }
}