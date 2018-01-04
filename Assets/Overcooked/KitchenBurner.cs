﻿//  --------------------------------------------------------------------------------------------------------------------
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
        /// Try to take an item
        /// </summary>
        /// <param name="item">Resulting item</param>
        /// <returns>True if an item was taken successfully</returns>
        public override bool TryTakeItemWithHand(out Holdable item)
        {
            if (this.CurrentContainer != null)
            {
                this.CurrentContainer.IsOnBurner = false;
                item = this.CurrentContainer;
                this.CurrentContainer = null;
                return true;
            }

            item = null;
            return false;
        }

        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="item">New item to be placed</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryPlaceItem(Holdable item)
        {
            // If there's nothing on top, only pans and pots can be placed
            if (this.CurrentContainer == null)
            {
                CookingContainer container;
                if (item is CookingPot)
                {
                    container = item as CookingPot;
                }
                else if (item is CookingPan)
                {
                    container = item as CookingPan;
                }
                else
                {
                    return false;
                }

                item.transform.position = this.transform.position + new Vector3(0, Config.ItemPlacementHeight);
                container.IsOnBurner = true;
                this.CurrentContainer = container;
                return true;
            }
            // There are stuff on the burner
            else
            {
                if (item is Ingredient)
                {
                    return false;
                }

                return this.CurrentContainer.TryAddIngredient(item as Ingredient);
            }
        }
    }
}
