using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemySystem enemySystem;
        [SerializeField] private BulletSystem _bulletSystem;
        //[SerializeField] private EnemyMoveAgent _moveAgent;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        

        public void Setup()
        {
            
        }

        private IMoveAgent _moveAgent;

        private void FixedUpdate()
        {

        }

        // private IEnumerator Start()
        // {
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(1);
        //         var enemy = this._enemyPool.SpawnEnemy();
        //         if (enemy != null)
        //         {
        //             if (this.m_activeEnemies.Add(enemy))
        //             {
        //                 enemy.GetComponent<HitPointsComponent>().HitPointsChanged += this.OnDestroyed;
        //                 enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
        //             }    
        //         }
        //     }
        // }
        //
        // private void OnDestroyed(GameObject enemy)
        // {
        //     if (m_activeEnemies.Remove(enemy))
        //     {
        //         enemy.GetComponent<HitPointsComponent>().HitPointsChanged -= this.OnDestroyed;
        //         enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;
        //
        //         _enemyPool.UnspawnEnemy(enemy);
        //     }
        // }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayerOwned = false,
                physicsLayer = (int) PhysicsLayer.CHARACTER,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });
        }
    }
}