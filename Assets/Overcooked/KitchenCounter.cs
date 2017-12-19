//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="KitchenCounter.cs">
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
    /// Describes the kitchen counter top
    /// </summary>
    public class KitchenCounter : OvercookedMapObject
    {
        /// <summary>
        /// The type of map object that this is
        /// </summary>
        public override OvercookedMapObjectTypes ObjectType
        {
            get
            {
                return OvercookedMapObjectTypes.Counter;
            }
        }

        /// <summary>
        /// The item that's current being placed on top
        /// </summary>
        protected Holdable CurrentlyPlaced;

        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="item">Item to be placed</param>
        /// <returns>True if the item can be placed on this map object</returns>
        public override bool TryPlaceItem(Holdable item)
        {
            if (this.CurrentlyPlaced != null)
            {
                if (item is Ingredient)
                {
                    return this.CurrentlyPlaced.AddIngredient(item as Ingredient);
                }
                else
                {
                    return false;
                }
            }

            this.CurrentlyPlaced = item;
            item.transform.position = this.transform.position + new Vector3(0, Config.ItemPlacementHeight);
            item.GetComponent<SpriteRenderer>().sortingOrder = -1;
            return true;
        }

        /// <summary>
        /// Try to take an item from the counter
        /// </summary>
        /// <param name="item">Resulting item</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryTakeItem(out Holdable item)
        {
            if (this.CurrentlyPlaced == null)
            {
                item = null;
                return false;
            }

            item = this.CurrentlyPlaced;
            this.CurrentlyPlaced = null;
            return true;
        }
    }
}
