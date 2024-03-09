using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public abstract class WeaponConfig : ScriptableObject
{
    public Weapon.Model Model => _model;
    [FormerlySerializedAs("model")]
    [FormerlySerializedAs("_type")] 
    [SerializeField] private Weapon.Model _model;
    
    public Weapon.Type Type => _type;
    [SerializeField] private Weapon.Type _type;
    
    public int Damage => _damage;
    [SerializeField] private int _damage;
}


