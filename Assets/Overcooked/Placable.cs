//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Placable.cs">
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
    /// Describes something that can have stuff placed on it
    /// </summary>
    public class Placable : MonoBehaviour
    {
        /// <summary>
        /// The item that's currently on top of this
        /// </summary>
        public Holdable CurrentlyOnTop;
    }
}
