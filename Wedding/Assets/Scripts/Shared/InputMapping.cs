//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InputMapping.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class InputMapping
    {
        /// <summary>
        /// A hash of controller index => input names
        /// </summary>
        private static Dictionary<int, InputNames> _controllerInputNames = new Dictionary<int, InputNames> ();

        /// <summary>
        /// Creates a static instance of the <see cref="InputMapping"/> class
        /// </summary>
        static InputMapping()
        {
            var burneyInputNames = new InputNames();
            burneyInputNames.XAxis = "BurneyXAxis";
            burneyInputNames.YAxis = "BurneyYAxis";
            burneyInputNames.AButton = "BurneyA";
            burneyInputNames.BButton = "BurneyB";
            burneyInputNames.XButton = "BurneyX";
            burneyInputNames.YButton = "BurneyY";
            burneyInputNames.LeftTrigger= "BurneyL";
            burneyInputNames.RightTrigger= "BurneyR";

            var linInputNames = new InputNames();
            linInputNames.XAxis = "LinXAxis";
            linInputNames.YAxis = "LinYAxis";
            linInputNames.AButton = "LinA";
            linInputNames.BButton = "LinB";
            linInputNames.XButton = "LinX";
            linInputNames.YButton = "LinY";
            linInputNames.LeftTrigger = "LinL";
            linInputNames.RightTrigger = "LinR";

            InputMapping._controllerInputNames[0] = burneyInputNames;
            InputMapping._controllerInputNames[1] = linInputNames;
        }

        /// <summary>
        /// Gets t he input names for the given controller index
        /// </summary>
        /// <param name="controllerIndex">Target controller index</param>
        /// <returns>Input names</returns>
        public static InputNames GetInputNames(int controllerIndex)
        {
            return InputMapping._controllerInputNames[controllerIndex];
        }
    }
}
