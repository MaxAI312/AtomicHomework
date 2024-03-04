using System;
using UnityEngine;

[Serializable]
public abstract class WeaponConfig : ScriptableObject
{
    public Weapon.Type Type => _type;
    [SerializeField] private Weapon.Type _type;
    
    public int Damage => _damage;
    [SerializeField] private int _damage;
}


