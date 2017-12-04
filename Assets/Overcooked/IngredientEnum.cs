//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IngredientEnum.cs">
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
    /// All types of ingredients used for 3 dishes
    /// * Onion soup
    /// * Burger (Bun, meat, lettuce)
    /// * Salad (Lettuce and tomato)
    /// * Everything except bun must be chopped before processing
    /// </summary>
    public enum IngredientEnum
    {
        Lettunce,
        Meat,
        Tomato,
        Onion,
        BurgerBun
    }
}
