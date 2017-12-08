//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LuxEProjectile.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describes Lux's E Projectile
    /// </summary>
    public class LuxEProjectile : MonoBehaviour
    {
        /// <summary>
        /// Range of the projectile
        /// </summary>
        public float Range;

        /// <summary>
        /// How fast the projectile travels
        /// </summary>
        public float Velocity;

        /// <summary>
        /// How much is left to travel
        /// </summary>
        public float DistanceToTravel;

        /// <summary>
        /// How long till it automatically detonates
        /// </summary>
        public float AutoDetonateTime;
       
        /// <summary>
        /// Detonates the projectile
        /// </summary>
        public void Detonate()
        {
            this.GetEnemiesInRange();
            foreach (var enemy in this.GetEnemiesInRange())
            {
                enemy.TakeDamage(Config.Lux.EDamage);
                enemy.ApplyEffect(EffectEnum.LuxMark, Config.Lux.MarkDuration);
            }

            Destroy(this.gameObject);
        }

        /// <summary>
        /// Called  once per frame
        /// </summary>
        protected void Update()
        {
            if (this.DistanceToTravel > 0)
            {
                var movementThisFrame = this.Velocity * Time.deltaTime;
                if (Math.Abs(movementThisFrame) < this.DistanceToTravel)
                {
                    this.transform.position += new Vector3(movementThisFrame, 0);
                    this.DistanceToTravel -= Math.Abs(movementThisFrame);
                }
                else
                {
                    this.transform.position += new Vector3(Math.Sign(this.Velocity) * this.DistanceToTravel, 0);
                    this.DistanceToTravel = 0;
                }
            }
            // Projectile has "landed" on the spot where it needs to be
            else
            {
                foreach (var enemy in this.GetEnemiesInRange())
                {
                    enemy.ApplyEffect(EffectEnum.Slow, 0.1f);
                }
            }

            this.AutoDetonateTime -= Time.deltaTime;
            if (this.AutoDetonateTime < 0)
            {
                this.Detonate();
            }
        }

        /// <summary>
        /// Update the enemies in range field
        /// </summary>
        private IEnumerable<EnemyController> GetEnemiesInRange()
        {
            var curX = this.transform.position.x;
            return EnemyController.Enemies.Where(enemy => Math.Abs(enemy.transform.position.x - curX) < this.Range);
        }
    }
}