//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DebuffEffect.cs">
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
    /// An effect that appears on top of the character with debuffs
    /// </summary>
    public class DebuffEffect :  MonoBehaviour
    {
        /// <summary>
        /// The target character
        /// </summary>
        public Character TargetCharacter;

        /// <summary>
        /// What this effect is representing
        /// </summary>
        public DebuffEnum TargetDebuff;

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            if (TargetCharacter.Debuffs.ContainsKey(this.TargetDebuff))
            {
                this.transform.position = this.TargetCharacter.transform.position;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
