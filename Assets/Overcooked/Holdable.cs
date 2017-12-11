//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Holdable.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overcooked
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describes something that can be held by the player
    /// </summary>
    public abstract class Holdable : MonoBehaviour
    {
        /// <summary>
        /// Gets the sprite renderer component
        /// </summary>
        public SpriteRenderer Sprite { get; private set; }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this.Sprite = this.GetComponent<SpriteRenderer>();
        }
    }
}
