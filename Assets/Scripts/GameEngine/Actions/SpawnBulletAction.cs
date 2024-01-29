using Atomic.Elements;
using UnityEngine;

public class SpawnBulletAction : IAtomicAction<Vector3>
{
    private readonly BulletConfig _bulletConfig;
    private readonly Transform _startPoint;

    public SpawnBulletAction(BulletConfig bulletConfig, Transform startPoint)
    {
        _bulletConfig = bulletConfig;
        _startPoint = startPoint;
    }
    
    public void Invoke(Vector3 direction)
    {
        Bullet instance = Object.Instantiate(_bulletConfig.Prefab, _startPoint.position, Quaternion.identity, null);
        instance.Setup(direction, _bulletConfig.LayerMask);
    }
}