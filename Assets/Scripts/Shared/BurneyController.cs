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
    public static class BurneyController
    {
        /// <summary>
        /// Index for the controller
        /// </summary>
        private static int _controllerIndex = 0;

        /// <summary>
        /// Gets the input schenma
        /// </summary>
        /// <returns></returns>
        public static InputNames ControlSchema
        {
            get
            {
                return InputMapping.GetInputNames(_controllerIndex);
            }
        }
    }
}
