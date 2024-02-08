using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Content
{
    public sealed class ConveyorDebug : MonoBehaviour
    {
        [SerializeField]
        private Conveyor conveyor;

        [Button]
        public void PutWood(int wood)
        {
            throw new NotImplementedException();
        }

        [Button]
        public int TakeLumber()
        {
            throw new NotImplementedException();
        }
    }
}