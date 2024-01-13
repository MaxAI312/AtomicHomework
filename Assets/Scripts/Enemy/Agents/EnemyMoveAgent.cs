using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveAgent
    {
        void SetDestination(Vector2 endPoint);
    }
    
    public sealed class EnemyMoveAgent : IFixedTickable, IMoveAgent
    {
        public bool IsReached
        {
            get { return isReached; }
        }

        private Vector2 destination;

        private bool isReached;

        private readonly Transform _transform;
        private readonly IMoveComponent _moveComponent;
        public EnemyMoveAgent(Transform transform, IMoveComponent moveComponent)
        {
            _transform = transform;
            _moveComponent = moveComponent;
        }
        
        //мне нужно получить свой трансформ

        public void SetDestination(Vector2 endPoint)// тоже интерфейс
        {
            destination = endPoint;
            isReached = false;
        }

        public void FixedTick()
        {
            // if (isReached)
            // {
            //     return;
            // }
            //
            // var vector = destination - (Vector2) _transform.position;
            // if (vector.magnitude <= 0.25f)
            // {
            //     isReached = true;
            //     return;
            // }
            //
            // var direction = vector.normalized * Time.fixedDeltaTime;
            // //_moveComponent.Move(direction);
        }
    }
}