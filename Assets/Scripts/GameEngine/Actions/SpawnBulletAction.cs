using Atomic.Elements;
using Homework3;
using UnityEngine;

public sealed class SpawnBulletAction : IAtomicAction
{
    private ObjectPool objectPool;
    private Transform _firePoint;
    
    public void Compose(ObjectPool objectPool, Transform firePoint)
    {
        this.objectPool = objectPool;
        _firePoint = firePoint;
    }
    
    public void Invoke()
    {
        GameObject bulletGameObject = objectPool.GetObject();
        bulletGameObject.transform.position = _firePoint.position;
        
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.SetupPoolMechanics(objectPool);
        bullet.Setup();
        
        bulletGameObject.SetActive(true);
    }
}
