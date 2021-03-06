﻿//  --------------------------------------------------------------------------------------------------------------------
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
    public abstract class OvercookedMapObject : MonoBehaviour, IContainer
    {
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected virtual void Start()
        {
            OvercookedGameController.CurrentInstance.AddMapObject(this);
        }

        /// <summary>
        /// Peek the content of the container
        /// </summary>
        /// <returns>The holdable item currently in this container</returns>
        public abstract IHoldable Peek();

        /// <summary>
        /// Take out the content
        /// </summary>
        /// <returns>The resulting content</returns>
        public abstract IHoldable RetrieveContent();

        /// <summary>
        /// Try to add a new item 
        /// </summary>
        /// <param name="newItem">New item to be added</param>
        /// <returns>True if successful</returns>
        public abstract bool TryAdd(IHoldable newItem);
    }
}
