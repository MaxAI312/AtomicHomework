using UnityEngine;

public sealed class CharacterInstaller : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private Character _character;

    private void Awake()
    {
        _entity.AddValue(ObjectAPI.MovementDirection, _character.Core.MoveComponent.MovementDirection);
        _entity.AddValue(ObjectAPI.FireAction, _character.Core.FireComponent.FireAction);
        _entity.AddValue(ObjectAPI.RotationDirection, _character.Core.RotationComponent.RotationDirection);

        //_entity.AddValue(ObjectAPI.WeaponStorage, _character.Core.WeaponStorage);
    }
}