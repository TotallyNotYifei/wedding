//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CookingPot.cs">
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
    /// Defines a cooking pot
    /// </summary>
    public class CookingPot : CookingContainer
    {
        /// <summary>
        /// Prefab for the soup ingredient
        /// </summary>
        public Ingredient SoupPrefab;

        /// <summary>
        /// Try to add an additional ingredient
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public override bool TryAdd(IHoldable newItem)
        {
            var newIngredient = newItem as Ingredient;
            if (newIngredient == null)
            {
                return false;
            }

            if (newIngredient.IngredientType != IngredientEnum.Onion || !newIngredient.IsChopped)
            {
                return false;
            }

            if (this.Ingredients.Count < 3)
            {
                this.Ingredients.Add(newIngredient);
                newIngredient.gameObject.SetActive(false);
                this.ProgressLimit = 0.334f * this.Ingredients.Count;
                this.ResetTimeTillBurn();
                this.ProgressBar.gameObject.SetActive(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Try to take out the content
        /// </summary>
        /// <returns>Resulting cooked food, null if not ready yet</returns>
        public override IHoldable RetrieveContent()
        {
            var result = this.Peek();
            if (result != null)
            {
                this.Ingredients = new List<Ingredient>();
                this.CookProgress = 0;
            }

            return result;
        }

        /// <summary>
        /// Try to peek the ingredient
        /// </summary>
        /// <returns></returns>
        public override IHoldable Peek()
        {
            if (this.CookProgress < 1)
            {
                return null;
            }

            return Instantiate(this.SoupPrefab).GetComponent<Ingredient>();
        }
    }
}
