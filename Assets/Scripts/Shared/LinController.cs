//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LinController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Controller for Lin
    /// </summary>
    public abstract class LinController
    {
        /// <summary>
        /// Index for the controller
        /// </summary>
        private static int _controllerIndex = 1;

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
