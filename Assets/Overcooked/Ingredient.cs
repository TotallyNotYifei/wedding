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
        /// If the ingrient is chopped
        /// </summary>
        private bool _isChopped;
    }
}
