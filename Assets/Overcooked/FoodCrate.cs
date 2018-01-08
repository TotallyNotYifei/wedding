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
        /// The next item to be grabbed
        /// </summary>
        private Ingredient _nextItem;

        /// <summary>
        /// Used for initialzation
        /// </summary>
        protected override void Start()
        {
            this.SpawnNew();
            base.Start();
        }

        /// <summary>
        /// Spawns a new ingredient and makes it invisible
        /// </summary>
        private void SpawnNew()
        {
            this._nextItem = Instantiate(IngredientPrefab);
            this._nextItem.gameObject.SetActive(false);
        }

        public override bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Peek the content
        /// </summary>
        /// <returns>Resulting item</returns>
        public override IHoldable Peek()
        {
            if (this.CurrentlyPlaced != null)
            {
                return base.Peek();
            }

            return _nextItem;
        }

        /// <summary>
        /// Retrieve the content
        /// </summary>
        /// <returns>result item</returns>
        public override IHoldable RetrieveContent()
        {
            if (this.CurrentlyPlaced != null)
            {
                return base.RetrieveContent();
            }

            var result = this._nextItem;
            result.gameObject.SetActive(true);
            return result;
        }
    }
}
