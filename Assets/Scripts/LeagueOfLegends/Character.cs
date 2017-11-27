//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Character.cs">
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
    /// A base class for all  league of legends characters
    /// </summary>
    public class Character :  MonoBehaviour
    {
        /// <summary>
        /// The base unit moved per second
        /// </summary>
        public float BaseSpeed;

        /// <summary>
        /// The acceleration
        /// </summary>
        public float Acceleration;

        /// <summary>
        /// A dictionary of active debuffs =>  duration left
        /// </summary>
        public IDictionary<DebuffEnum, float> Debuffs { get; private set; }

        /// <summary>
        /// Adds a debuff
        /// </summary>
        /// <param name="debuff">Target debuff</param>
        /// <param name="duration">How much total duration for the debuff</param>
        public void ApplyDebuff(DebuffEnum debuff, float duration)
        {
            float existingDebuff;
            if (this.Debuffs.TryGetValue(debuff, out existingDebuff))
            {
                if (existingDebuff > duration)
                {
                    return;
                }
            }

            this.Debuffs[debuff] = duration;
        }
    }
}
