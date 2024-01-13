using UnityEngine;

namespace ShootEmUp
{
    public interface IFireMechanics
    {
        void Fire();
    }

    public class FireMechanics : IFireMechanics
    {
        public void Fire()
        {
            
        }
        
        // private void CreateBullet()
        // {
        //     var weapon = character.GetComponent<WeaponComponent>();
        //     _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
        //     {
        //         isPlayerOwned = true,
        //         physicsLayer = (int) _bulletConfig.PhysicsLayer,
        //         color = _bulletConfig.Color,
        //         damage = _bulletConfig.Damage,
        //         position = weapon.Position,
        //         velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
        //     });
        // }
    }
}