using Atomic.Elements;
using UnityEngine;

public sealed class FireCondition : IAtomicValue<bool>
{
    public bool Value => _isAlive.Value && _charges.Value > 0 && _gameObject.activeSelf;

    private IAtomicValue<bool> _isAlive;
    private IAtomicValue<int> _charges;
    private GameObject _gameObject;

    public void Compose(
        IAtomicValue<bool> isAlive,
        IAtomicValue<int> charges,
        GameObject gameObject)
    {
        _isAlive = isAlive;
        _charges = charges;
        _gameObject = gameObject;
    }
}
