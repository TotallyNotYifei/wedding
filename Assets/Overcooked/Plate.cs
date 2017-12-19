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
    public class Plate : Container
    {
        /// <summary>
        /// Gets the plate type
        /// </summary>
        public override HoldableTypes HoldableType
        {
            get
            {
                return HoldableTypes.Plate;
            }
        }

        /// <summary>
        /// Consumes the given ingredient
        /// </summary>
        /// <param name="ingredeint">Target ingredient</param>
        private void ConsumeIngredient(Ingredient ingredeint)
        {
            this.Ingredeints.Add(ingredeint);
        }

        private static HashSet<IngredientEnum> _singleOnlyItems = new HashSet<IngredientEnum>() {
            IngredientEnum.RawMeat,
            IngredientEnum.Onion,
            IngredientEnum.OnionSoup };

        /// <summary>
        /// Try to add a new ingredient to a plate
        /// </summary>
        /// <param name="newIngredient">New ingredient to be added</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryAddIngredient(Ingredient newIngredient)
        {
            if (!newIngredient.IsChopped)
            {
                return false;
            }

            if (this.Ingredeints.Any(existIng => existIng.IngredientType == newIngredient.IngredientType))
            {
                return false;
            }

            // Check for single plate stuff
            if (this.Ingredeints.Count != 0 && _singleOnlyItems.Contains(newIngredient.IngredientType))
            {
                return false;
            }

            this.Ingredeints.Add(newIngredient);
            newIngredient.transform.parent = this.transform;
            newIngredient.transform.localPosition = UnityEngine.Vector3.zero;

            return true;
        }
    }
}
