//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="OvercookedMapObject.cs">
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
    /// Defines an object on the map
    /// </summary>
    public abstract class OvercookedMapObject : MonoBehaviour
    {
        /// <summary>
        /// The type of object that this is
        /// </summary>
        public abstract OvercookedMapObjectTypes ObjectType { get; }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            OvercookedGameController.CurrentInstance.AddMapObject(this);
        }

        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="item">Item to be placed</param>
        /// <returns>True if the operation succed, and the item has been placed</returns>
        public virtual bool TryPlaceItem(Holdable item)
        {
            return false;
        }

        /// <summary>
        /// Try to take an item from this map object
        /// </summary>
        /// <param name="item">Item reterieve</param>
        /// <returns>True if the operation succed, and the item has been taken off</returns>
        public virtual bool TryTakeItem(out Holdable item)
        {
            item = null;
            return false;
        }
    }
}
