using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class SpawnBulletAction : IAtomicAction<Vector3>
{
    private BulletConfig _bulletConfig;
    private Transform _startPoint;
    
    public void Compose(BulletConfig bulletConfig, Transform startPoint)
    {
        _bulletConfig = bulletConfig;
        _startPoint = startPoint;
    }
    
    [Button]
    public void Invoke(Vector3 direction)
    {
        Bullet instance = Object.Instantiate(_bulletConfig.Prefab, _startPoint.position, Quaternion.identity, null);
        instance.Setup(direction, _bulletConfig.LayerMask);
    }
}