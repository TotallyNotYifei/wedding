//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHoldable.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overcooked
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IHoldable
    {

        /// <summary>
        /// Sets the display layer of the container
        /// </summary>
        /// <param name="newLayer">the new layer id</param>
        void SetDisplayLayer(int newLayer);

        /// <summary>
        /// Dumps the content
        /// </summary>
        void Dump();
    }
}
