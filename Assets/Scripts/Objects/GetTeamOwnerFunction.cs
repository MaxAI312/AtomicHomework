using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class GetTeamOwnerFunction : IAtomicFunction<TeamType>
{
    private IAtomicValue<IAtomicObject> _owner;
    
    public GetTeamOwnerFunction()
    {
        
    }

    public GetTeamOwnerFunction(IAtomicValue<IAtomicObject> owner)
    {
        _owner = owner;
    }

    public void Compose(IAtomicValue<IAtomicObject> owner)
    {
        _owner = owner;
    }
    
    [Button]
    public TeamType Invoke()
    {
        IAtomicObject owner = _owner.Value;
        if (owner == null)
        {
            return TeamType.UNDEFIEND;
        }
        
        return owner.GetValue<TeamType>(TeamAPI.Team).Value;
    }
}