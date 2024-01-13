using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IInputService
    {
        float HorizontalDirection { get; }
        event Action ClickedFireButton;
    }
    
    public sealed class InputService : ITickable, IFixedTickable, IInputService
    {
        private Player _player;
        
        public event Action ClickedFireButton;
        
        public float HorizontalDirection { get; private set; }

        public void Setup(Player player)
        {
            _player = player;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.LogError("Space");
                _player._fireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
                Debug.LogError(HorizontalDirection + "LeftArrow");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
                Debug.LogError(HorizontalDirection + "RightArrow");
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
        
        public void FixedTick()
        {
            //_player.GetComponent<IMoveComponent>().Move(new Vector2(HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}