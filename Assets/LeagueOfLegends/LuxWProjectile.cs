//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LuxWProjectile.cs">
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
    /// Describes the Lux's W boomerang projectile
    /// </summary>
    public class LuxWProjectile : MonoBehaviour
    {
        /// <summary>
        /// The real lux
        /// </summary>
        public LuxController Lux;

        /// <summary>
        /// The speed of the projectile
        /// </summary>
        public float Velocity;
    
        /// <summary>
        /// How much time is left until the projectile bounces back
        /// </summary>
        public float Duration;

        /// <summary>
        /// Effect visuals for the shield
        /// </summary>
        public EffectVisuals ShieldEffectPrefab;

        /// <summary>
        /// How long the projectile will keep going forward
        /// </summary>
        private bool _hasReturned;

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._hasReturned = false;
        } 

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            if (this.Duration > 0)
            {
                this.Duration -= Time.deltaTime;
                if (this.Duration <= 0)
                {
                    this.Velocity *= -1;
                    this._hasReturned = true;
                }
            }

            if (this._hasReturned && Math.Abs(this.Lux.transform.position.x -this.transform.position.x) < Config.Lux.LuxCatchReturningWRange )
            {
                Destroy(this.gameObject);
            }

            this.transform.position += new Vector3(this.Velocity * Time.deltaTime, 0);
        }
    }
}
