using Atomic.Elements;
using Homework3;
using UnityEngine;

public sealed class SpawnBulletAction : IAtomicAction
{
    private ObjectPool _objectPool;
    private Transform _firePoint;
    
    public void Compose(ObjectPool objectPool, Transform firePoint)
    {
        _objectPool = objectPool;
        _firePoint = firePoint;
    }
    
    public void Invoke()
    {
        Debug.Log(_objectPool + " - ObjectPool SPAWNBULLET");
        GameObject bulletGameObject = _objectPool.GetObject();
        bulletGameObject.transform.position = _firePoint.position;
        
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.Construct(_objectPool);
        bullet.Setup();
        bullet.Cooldown.Start();
        
        bulletGameObject.SetActive(true);
    }
}
