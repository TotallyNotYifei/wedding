//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IContainer.cs">
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
    /// Defines a container
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Peek the content of the container
        /// </summary>
        /// <returns>The holdable item currently in this container</returns>
        IHoldable Peek();

        /// <summary>
        /// Take out the content
        /// </summary>
        /// <returns>The resulting content</returns>
        IHoldable RetrieveContent();

        /// <summary>
        /// Try to add a new item 
        /// </summary>
        /// <param name="newItem">New item to be added</param>
        /// <returns>True if successful</returns>
        bool TryAdd(IHoldable newItem);

        /// <summary>
        /// Gets a value indicating whether the plate is empty or not
        /// </summary>
        bool IsEmpty { get; }
    }
}
