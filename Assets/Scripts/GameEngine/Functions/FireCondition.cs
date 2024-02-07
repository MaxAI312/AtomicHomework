using Atomic.Elements;

public sealed class FireCondition : IAtomicValue<bool>
{
    public bool Value => _isAlive.Value && _charges.Value > 0;

    private IAtomicValue<bool> _isAlive;
    private IAtomicValue<int> _charges;

    public void Compose(IAtomicValue<bool> isAlive, IAtomicValue<int> charges)
    {
        _isAlive = isAlive;
        _charges = charges;
    }
}
