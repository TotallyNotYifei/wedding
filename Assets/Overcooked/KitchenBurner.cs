//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="KitchenBurner.cs">
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
    /// Describes the kitchen burner
    /// </summary>
    public class KitchenBurner : KitchenCounter
    {
        /// <summary>
        /// The current cooking container on top
        /// </summary>
        public CookingContainer CurrentContainer
        {
            get
            {
                return this.CurrentlyPlaced as CookingContainer;
            }
            set
            {
                this.CurrentlyPlaced = value;
            }
        }


        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="newItem">New item to be placed</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryAdd(IHoldable newItem)
        {
            // If there's nothing on top, only pans and pots can be placed
            if (this.CurrentContainer != null)
            {
                return false;
            }
            var newCookingContainer = newItem as CookingContainer;
            if (newCookingContainer == null)
            {
                return false;
            }

            newCookingContainer.transform.position = this.transform.position + new Vector3(0, Config.ItemPlacementHeight);
            newCookingContainer.IsOnBurner = true;
            this.CurrentContainer = newCookingContainer;
            return true;
        }

        public override IHoldable RetrieveContent()
        {
            this.CurrentContainer.IsOnBurner = false;
            return base.RetrieveContent();
        }
    }
}
