//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HoldableContainer.cs">
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
    public abstract class HoldableContainer : Holdable, IContainer
    {
        /// <summary>
        /// A list of ingredients in this container
        /// </summary>
        public List<Ingredient> Ingredients = new List<Ingredient>();

        public virtual bool IsEmpty
        {
            get
            {
                return this.Ingredients.Count > 0;
            }
        }

        public abstract IHoldable Peek();

        public abstract IHoldable RetrieveContent();

        public abstract bool TryAdd(IHoldable newItem);

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected virtual void Update()
        {
            foreach (var ingredient in this.Ingredients)
            {
                ingredient.transform.position = this.transform.position;
            }
        }

        public override void SetDisplayLayer(int newLayer)
        {
            foreach (var ingredient in this.Ingredients)
            {
                ingredient.SetDisplayLayer(newLayer + 1);
            }

            base.SetDisplayLayer(newLayer);
        }
    }
}
