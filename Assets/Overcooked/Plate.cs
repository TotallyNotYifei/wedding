//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Plate.cs">
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
    /// Describes a plate
    /// </summary>
    public class Plate : HoldableContainer
    {
        /// <summary>
        /// Consumes the given ingredient
        /// </summary>
        /// <param name="ingredeint">Target ingredient</param>
        private void ConsumeIngredient(Ingredient ingredeint)
        {
            this.Ingredients.Add(ingredeint);
        }

        private static HashSet<IngredientEnum> _singleOnlyItems = new HashSet<IngredientEnum>() {
            IngredientEnum.RawMeat,
            IngredientEnum.Onion,
            IngredientEnum.OnionSoup };

        private static HashSet<IngredientEnum> _choppedOnlyitems = new HashSet<IngredientEnum>() {
            IngredientEnum.Lettunce,
            IngredientEnum.Onion,
            IngredientEnum.RawMeat };

        /// <summary>
        /// Try to add a new ingredient to a plate
        /// </summary>
        /// <param name="newItem">New ingredient to be added</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryAdd(IHoldable newItem)
        {
            var newIngredient = newItem as Ingredient;
            if (newIngredient == null)
            {
                return false;
            }

            if (_choppedOnlyitems.Contains(newIngredient.IngredientType) && !newIngredient.IsChopped)
            {
                return false;
            }

            if (this.Ingredients.Any(existIng => existIng.IngredientType == newIngredient.IngredientType))
            {
                return false;
            }

            // Check for single plate stuff
            if (this.Ingredients.Count != 0 && _singleOnlyItems.Contains(newIngredient.IngredientType))
            {
                return false;
            }

            this.Ingredients.Add(newIngredient);
            newIngredient.transform.parent = this.transform;
            newIngredient.transform.localPosition = UnityEngine.Vector3.zero;

            return true;
        }

        public override IHoldable Peek()
        {
            if (this.Ingredients.Count != 0)
            {
                return null;
            }

            return this.Ingredients[0];
        }

        public override IHoldable RetrieveContent()
        {
            var result = this.Peek();
            this.Ingredients = new List<Ingredient>();
            return result;
        }
    }
}
