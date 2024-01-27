using Atomic.Elements;
using Homework3;
using UnityEngine;

public sealed class SpawnBulletAction : IAtomicAction
{
    private ObjectPoolMechanics _objectPoolMechanics;
    private Transform _firePoint;
    
    public void Compose(ObjectPoolMechanics objectPoolMechanics, Transform firePoint)
    {
        _objectPoolMechanics = objectPoolMechanics;
        _firePoint = firePoint;
    }
    
    public void Invoke()
    {
        GameObject bulletGameObject = _objectPoolMechanics.GetObject();
        bulletGameObject.transform.position = _firePoint.position;
        
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.SetupPoolMechanics(_objectPoolMechanics);
        bullet.Setup();
        
        bulletGameObject.SetActive(true);
    }
}
