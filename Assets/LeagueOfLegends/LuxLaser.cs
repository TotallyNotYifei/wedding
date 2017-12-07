//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LuxLaser.cs">
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
    /// Describes the lux laser behaviour
    /// </summary>
    public class LuxLaser : MonoBehaviour
    {
        /// <summary>
        /// The lux controller
        /// </summary>
        public LuxController Lux;

        /// <summary>
        /// How long until the laser detonates
        /// </summary>
        public float TimeTillDetonate;

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            if (this.TimeTillDetonate > 0)
            {
                this.TimeTillDetonate -= Time.deltaTime;
                if (this.TimeTillDetonate <= 0)
                {
                    this.Lux.OnRFire();
                }
            }
        }
    }
}
