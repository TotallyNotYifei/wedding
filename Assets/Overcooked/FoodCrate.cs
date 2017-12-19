//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FoodCrate.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overcooked
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The food crate
    /// </summary>
    public class FoodCrate : KitchenCounter
    {
        /// <summary>
        /// Prefab for the ingredient
        /// </summary>
        public Ingredient IngredientPrefab;

        /// <summary>
        /// Gets the type of object that the crate is
        /// </summary>
        public override OvercookedMapObjectTypes ObjectType
        {
            get
            {
                return OvercookedMapObjectTypes.FoodCrate;
            }
        }

        /// <summary>
        /// Try to grab an item from here
        /// </summary>
        /// <param name="item">Result item</param>
        /// <returns>True if operation successful</returns>
        public override bool TryTakeItem(out Holdable item)
        {
            // If there's something on top, treat it as a counter
            if (this.CurrentlyPlaced != null)
            {
                return base.TryTakeItem(out item);
            }

            var newIngredient = Instantiate(this.IngredientPrefab);
            item = newIngredient;
            return true;
        }
    }
}
