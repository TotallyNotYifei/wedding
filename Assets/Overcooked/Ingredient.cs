//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Ingredient.cs">
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
    /// Describes an ingredient
    /// </summary>
    public class Ingredient : Holdable
    {
        /// <summary>
        /// The ingredient
        /// </summary>
        public IngredientEnum IngredientType;

        /// <summary>
        /// Sprite for an ingredient that's been shopped
        /// </summary>
        public Sprite ChoppedSprite;

        /// <summary>
        /// If the ingredient has been chopped on a chopping board
        /// </summary>
        public bool IsChopped
        {
            get
            {
                return this._isChopped;
            }
            set
            {
                this._isChopped = value;
            }
        }

        /// <summary>
        /// If the ingredient is chopped
        /// </summary>
        private bool _isChopped;

        /// <summary>
        /// When the ingredient is chopped
        /// </summary>
        public void FinishChopping()
        {
            this.SpriteComponent.sprite = this.ChoppedSprite;
            this._isChopped = true;
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            this._isChopped = false;
            base.Start();
        }
    }
}
