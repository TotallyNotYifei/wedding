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
        public override HoldableTypes HoldableType
        {
            get
            {
                return HoldableTypes.Pot;
            }
        }

        /// <summary>
        /// Try to add an additional ingredient
        /// </summary>
        /// <param name="newIngredient"></param>
        /// <returns></returns>
        public override bool TryAddIngredient(Ingredient newIngredient)
        {
            if (newIngredient.IngredientType != IngredientEnum.Onion || !newIngredient.IsChopped)
            {
                return false;
            }

            if (this.Ingredeints.Count < 3)
            {
                this.Ingredeints.Add(newIngredient);
                this.ProgressLimit = 0.334f * this.Ingredeints.Count;
                this.ResetTimeTillBurn();
                this.ProgressBar.gameObject.SetActive(true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
