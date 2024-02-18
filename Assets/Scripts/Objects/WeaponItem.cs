using System.Collections;
using System.Collections.Generic;
using Atomic.Objects;
using UnityEngine;

public class WeaponItem : AtomicObject
{
    public WeaponConfig Config => _config;
    [SerializeField] private WeaponConfig _config;
    
    
}
