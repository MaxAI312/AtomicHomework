using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveComponent
    {
        void Move(float horizontalDirection);
    }

    public sealed class MoveComponent : IMoveComponent
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _speed;

        public MoveComponent(Rigidbody2D rigidbody2D, float speed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }
        
        public void Move(float horizontalDirection)
        {
            var nextPosition = _rigidbody2D.position + new Vector2(horizontalDirection, 0) * _speed;
            _rigidbody2D.MovePosition(nextPosition);
            Debug.LogError(nextPosition + " - Move");
        }
    }
}