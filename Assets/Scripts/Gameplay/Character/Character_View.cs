using System;
using UnityEngine;

[Serializable]
public sealed class Character_View
{
    [SerializeField] private Animator _animator;

    private MoveAnimMechanics _moveAnimMechanics;
    private FireAnimMechanics _fireAnimMechanics;
    
    public void Compose(Character_Core core)
    {
        _moveAnimMechanics = new MoveAnimMechanics(_animator, core.MoveComponent.IsMoving);
        _fireAnimMechanics = new FireAnimMechanics(_animator, core.FireComponent.FireEvent);
    }

    public void OnEnable()
    {
        _fireAnimMechanics.OnEnable();
    }

    public void OnDisable()
    {
        _fireAnimMechanics.OnDisable();
    }

    public void Update()
    {
        _moveAnimMechanics.Update();
    }
}
