using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public int Damage => _damage;
        private int _damage;

        public bool IsPlayerOwned => _isPlayerOwned;
        private bool _isPlayerOwned;
        
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void SetStatusIsPlayerOwned(bool value)
        {
            _isPlayerOwned = value;
        }
    }
}