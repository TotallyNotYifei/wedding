//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CookingPan.cs">
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
    /// Describes a cooking pan
    /// </summary>
    public class CookingPan : CookingContainer
    {
        /// <summary>
        /// Sprite for the cooked meat
        /// </summary>
        public Sprite CookedMeatSprite;

        /// <summary>
        /// Try to add an ingredient to the pan
        /// </summary>
        /// <param name="newIngredient">new ingredient to be added</param>
        /// <returns>True if ingredient was added successfull</returns>
        public override bool TryAdd(IHoldable newIngredient)
        {
            var newIngObj = newIngredient as Ingredient;
            if (newIngObj == null)
            {
                return false;
            }
            if (this.Ingredients.Count > 0)
            {
                return false;
            }

            if (newIngObj.IngredientType == IngredientEnum.RawMeat && newIngObj.IsChopped)
            {

                this.Ingredients.Add(newIngObj);
                newIngObj.transform.position = this.transform.position;
                this.ProgressBar.gameObject.SetActive(true);
                this.ProgressLimit = 1;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to take out the content
        /// </summary>
        /// <returns>Ingredient if available</returns>
        public override IHoldable RetrieveContent()
        {
            var result = this.Peek();
            if (result != null)
            {
                this.Ingredients.RemoveAt(0);
                return result;
            }

            return null;
        }

        /// <summary>
        /// Called when finished cooking
        /// </summary>
        protected override void OnFinishCookingHook()
        {
            this.Ingredients[0].GetComponent<SpriteRenderer>().sprite = this.CookedMeatSprite;
        }

        /// <summary>
        /// Peeks at the ingredient inside
        /// </summary>
        /// <returns></returns>
        public override IHoldable Peek()
        {
            if (this.Ingredients.Count == 0)
            {
                return null;
            }

            if (this.CookProgress < 1)
            {
                return null;
            }

            return this.Ingredients[0];
        }
    }
}
