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
    public abstract class Holdable : MonoBehaviour, IHoldable
    {
        /// <summary>
        /// Gets the sprite renderer component
        /// </summary>
        public SpriteRenderer SpriteComponent { get; private set; }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected virtual void Start()
        {
            this.SpriteComponent = this.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Sets the display layer of the container
        /// </summary>
        /// <param name="newLayer">the new layer id</param>
        public virtual void SetDisplayLayer(int newLayer)
        {
            this.SpriteComponent.sortingOrder = newLayer;
        }
    }
}
