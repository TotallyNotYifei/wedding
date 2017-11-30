//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AutoSelfDestroy.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describes an object  that destroys itself after a set amount of time
    /// </summary>
    public class AutoSelfDestroy : MonoBehaviour
    {
        /// <summary>
        /// How much time is left until self destroy
        /// </summary>
        public float Timer;

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            this.Timer -= Time.deltaTime;
            if (this.Timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
