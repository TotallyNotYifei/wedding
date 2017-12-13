//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Container.cs">
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
    /// Describes a container such as plate, pot, or pan
    /// </summary>
    public class Container : Holdable
    {
        /// <summary>
        /// A list of ingredients in this container
        /// </summary>
        public HashSet<Ingredient> Ingredeints = new HashSet<Ingredient>();
    }
}
