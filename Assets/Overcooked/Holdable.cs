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
        /// Gets the type of holdable this item is
        /// </summary>
        public abstract HoldableTypes HoldableType { get; }

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
        /// Adds an ingredient to this item
        /// </summary>
        /// <param name="newIngredient">New ingredient to be added</param>
        /// <returns>True if operation successful</returns>
        public virtual bool TryAddIngredient(Ingredient newIngredient)
        {
            return false;
        }
    }
}
