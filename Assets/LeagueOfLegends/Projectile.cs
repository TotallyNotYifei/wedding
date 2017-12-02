//  --------------------------------------------------------------------------------------------------------------------
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
        public EffectEnum CarriedEffect;

        /// <summary>
        /// The visual that will display when the target is hit
        /// </summary>
        public EffectVisuals EffectVisualPrefab;

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
