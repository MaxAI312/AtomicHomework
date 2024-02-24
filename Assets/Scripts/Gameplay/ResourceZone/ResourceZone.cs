using Atomic.Elements;
using UnityEngine;

namespace Content
{
    public sealed class ResourceZone
    {
        private int _current;

        public AtomicEvent<int> Changed = new();
        
        public int Max { get; }
        
        public int Current
        {
            get => _current;
            set
            {
                _current = Mathf.Clamp(value, 0, Max);
                Changed?.Invoke(_current);
            }
        }
        
        public ResourceZone(int current, int max)
        {
            _current = current;
            Max = max;
        }
    }
}