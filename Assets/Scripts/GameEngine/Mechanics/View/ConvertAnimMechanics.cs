using Atomic.Elements;
using UnityEngine;

namespace Content
{
    public sealed class ConvertAnimMechanics
    {
        private static readonly int IsWorking = Animator.StringToHash("IsWorking");
        
        private readonly Animator _animator;
        private readonly IAtomicObservable<bool> _observable;

        public ConvertAnimMechanics(Animator animator, IAtomicObservable<bool> observable)
        {
            _animator = animator;
            _observable = observable;
        }

        public void OnEnable()
        {
            _observable.Subscribe(Play);
            Play(true);
        }

        public void OnDisable()
        {
            _observable.Unsubscribe(Play);
        }

        private void Play(bool value)
        {
            _animator.SetBool(IsWorking, value);
        }
    }
}