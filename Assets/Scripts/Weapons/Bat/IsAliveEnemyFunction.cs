using Atomic.Elements;
using Atomic.Objects;

public class IsAliveEnemyFunction : IAtomicFunction<IAtomicObject, bool>
{
    private IAtomicValue<TeamType> _myTeam;
    public void Compose(IAtomicValue<TeamType> myTeam)
    {
        _myTeam = myTeam;
    }

    public bool Invoke(IAtomicObject target)
    {
        if (target == null)
        {
            return false;
        }

        return target.TryGet(LifeAPI.IsAlive, out IAtomicValue<bool> isAlive) && isAlive.Value;
    }
}