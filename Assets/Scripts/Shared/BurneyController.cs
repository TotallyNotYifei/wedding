//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BurneyController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Controller for Burney
    /// </summary>
    public abstract class BurneyController : BaseController
    {
        /// <summary>
        /// Index for the controller
        /// </summary>
        protected override int _controllerIndex
        {
            get
            {
                return 0;
            }
        }
    }
}
