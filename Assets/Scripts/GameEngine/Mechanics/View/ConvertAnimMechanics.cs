using System;
using Atomic.Elements;
using UnityEngine;

namespace Content
{
    public sealed class ConvertAnimMechanics
    {
        private static readonly int IsWorking = Animator.StringToHash("IsWorking");
        
        private readonly Animator _animator;
        private readonly IAtomicObservable _observable;

        public ConvertAnimMechanics(Animator animator, IAtomicObservable observable)
        {
            _animator = animator;
            _observable = observable;
        }

        public void OnEnable()
        {
            Play();
            _observable.Subscribe(Play);
        }

        public void OnDisable()
        {
            _observable.Unsubscribe(Play);
        }

        private void Play()
        {
            _animator.SetBool(IsWorking, true);
        }
    }
}