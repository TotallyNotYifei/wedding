//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DashTarget.cs">
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
    /// Potential dash target for LeeSin
    /// </summary>
    public class DashTarget : MonoBehaviour
    {
        /// <summary>
        /// A list of dash targets
        /// </summary>
        public static List<DashTarget> Targets = new List<DashTarget>();

        /// <summary>
        /// Called once in the beginning
        /// </summary>
        protected void Start()
        {
            DashTarget.Targets.Add(this);
        }

        protected void OnDestroy()
        {
            DashTarget.Targets.Remove(this);
        }
    }
}
