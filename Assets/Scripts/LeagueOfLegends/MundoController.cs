//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MundoController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Controls the NPC Mundo
    /// </summary>
    public class MundoController : Character
    {
        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            // Decay the debuffs
            var timePassed = Time.deltaTime;
            foreach (var item in this.Debuffs)
            {
                var key = item.Key;
                if (item.Value < Time.deltaTime)
                {
                    this.Debuffs.Remove(key);
                }
                else
                {
                    this.Debuffs[key] = item.Value - Time.deltaTime;
                }
            }

            // Move if not snared
            if (!this.Debuffs.ContainsKey(DebuffEnum.Snare))
            {
                var movementThisFrame = this.BaseSpeed * Time.deltaTime;
                if (this.Debuffs.ContainsKey(DebuffEnum.Slow))
                {
                    movementThisFrame /= 2;
                }

                this.transform.position += new Vector3(movementThisFrame, 0);
            }
        }
    }
}
