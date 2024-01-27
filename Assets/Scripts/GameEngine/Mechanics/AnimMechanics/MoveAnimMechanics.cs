using Atomic.Elements;
using UnityEngine;

public sealed class MoveAnimMechanics
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private readonly Animator _animator;
    private readonly IAtomicValue<bool> _isMoving;

    public MoveAnimMechanics(Animator animator, IAtomicValue<bool> isMoving)
    {
        _animator = animator;
        _isMoving = isMoving;
    }

    public void Update()
    {
        _animator.SetBool(IsMoving, _isMoving.Value);
    }
}
