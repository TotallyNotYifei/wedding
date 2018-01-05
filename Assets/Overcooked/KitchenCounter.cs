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
        /// The item that's current being placed on top
        /// </summary>
        protected Holdable CurrentlyPlaced { get; set; }

        public override bool IsEmpty
        {
            get
            {
                return this.CurrentlyPlaced == null;
            }
        }

        public override IHoldable Peek()
        {
            return this.CurrentlyPlaced;
        }

        public override IHoldable RetrieveContent()
        {
            var result = this.CurrentlyPlaced;
            this.CurrentlyPlaced = null;
            return result;
        }

        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="item">Item to be placed</param>
        /// <returns>True if the item can be placed on this map object</returns>
        public override bool TryAdd(IHoldable item)
        {
            var itemObj = item as Holdable;

            if (!itemObj)
            {
                return false;
            }

            if (this.CurrentlyPlaced != null)
            {
                var currentPlacecdContainer = this.CurrentlyPlaced as IContainer;
                if (currentPlacecdContainer != null)
                {
                    return currentPlacecdContainer.TryAdd(item as Ingredient);
                }
                else
                {
                    return false;
                }
            }

            this.CurrentlyPlaced = itemObj;
            itemObj.transform.position = this.transform.position + new Vector3(0, Config.ItemPlacementHeight);
            return true;
        }
    }
}
