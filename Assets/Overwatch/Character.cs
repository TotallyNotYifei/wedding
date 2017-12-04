//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Character.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overwatch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Defines a character in Overwatch
    /// </summary>
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The total health for a character
        /// </summary>
        public float TotalHealth;

        /// <summary>
        /// Gets the current health for a character
        /// </summary>
        public float CurrentHealth { get; private set; }

        /// <summary>
        /// Takes damage
        /// </summary>
        /// <param name="damage">Amount of damage taken</param>
        public void TakeDamage(float damage)
        {
            this.CurrentHealth -= damage;
            this.CurrentHealth = Math.Max(this.CurrentHealth, 0);
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected virtual void Start()
        {
            this.CurrentHealth = this.TotalHealth;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected virtual void Update()
        {
        }
    }
}
