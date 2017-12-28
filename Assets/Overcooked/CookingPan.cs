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
        /// The type of holdable that this is
        /// </summary>
        public override HoldableTypes HoldableType
        {
            get
            {
                return HoldableTypes.Pan;
            }
        }

        /// <summary>
        /// Try to add an ingredient to the pan
        /// </summary>
        /// <param name="newIngredient">new ingredient to be added</param>
        /// <returns>True if ingredient was added successfull</returns>
        public override bool TryAddIngredient(Ingredient newIngredient)
        {
            if (this.Ingredeints.Count > 0)
            {
                return false;
            }

            if (newIngredient.IngredientType != IngredientEnum.RawMeat || !newIngredient.IsChopped)
            {
                return false;
            }

            this.Ingredeints.Add(newIngredient);
            newIngredient.transform.position = this.transform.position;
            this.ProgressBar.gameObject.SetActive(true);
            this.ProgressLimit = 1;
            return true;
        }

        /// <summary>
        /// Try to take out the content
        /// </summary>
        /// <returns>Ingredient if available</returns>
        public override Ingredient TryTakeoutContent()
        {
            if (this.Ingredeints.Count == 0)
            {
                return null;
            }

            if (this.CookProgress < 1)
            {
                return null;
            }

            var result = this.Ingredeints[0];
            this.Ingredeints.RemoveAt(0);
            return result;
        }

        /// <summary>
        /// Called when finished cooking
        /// </summary>
        protected override void OnFinishCookingHook()
        {
            this.Ingredeints[0].GetComponent<SpriteRenderer>().sprite = this.CookedMeatSprite;
        }
    }
}
