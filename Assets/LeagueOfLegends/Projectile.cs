﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Projectile.cs">
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
    /// Describes any projectile
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// The effect of this projectile
        /// </summary>
        public List<EffectEnum> CarriedEffects;

        /// <summary>
        /// The visual that will display when the target is hit
        /// </summary>
        public List<EffectVisuals> EffectVisualPrefabs;

        /// <summary>
        /// How long the effect will last
        /// </summary>
        public float EffectDuration;

        /// <summary>
        /// Speed of the projectile in units  per second
        /// </summary>
        public float Velocity;

        /// <summary>
        /// How long this projectile lasts
        /// </summary>
        public float Duration;

        /// <summary>
        /// How many targets this projectile can hit
        /// </summary>
        public int HitCount;

        /// <summary>
        /// How much damage this projectile can do
        /// </summary>
        public float Damage;

        /// <summary>
        /// Called when the projectile hits an enemy
        /// </summary>
        public void OnHittingEnemy()
        {
            this.HitCount--;
            if (this.HitCount <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            this.Duration -= Time.deltaTime;
            if (this.Duration < 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.transform.position += new Vector3(Velocity * Time.deltaTime, 0, 0);
            }
        }
    }
}
