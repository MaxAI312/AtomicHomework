using System;
using Atomic.Objects;

[Serializable]
public struct TakeDamageArgs
{
    public int Damage;
    public IAtomicObject Owner;

    public TakeDamageArgs(
        int damage,
        IAtomicObject owner = null)
    {
        Damage = damage;
        Owner = owner;
    }
}