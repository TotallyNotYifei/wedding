//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Config.cs">
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
    /// A collection of static configurations
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Used for initialization
        /// </summary>
        static Config()
        {
            DirectionToAnimatorParam = new Dictionary<DirectionEnum, string>();
            DirectionToAnimatorParam[DirectionEnum.Up] = "IsUp";
            DirectionToAnimatorParam[DirectionEnum.Left] = "IsLeft";
            DirectionToAnimatorParam[DirectionEnum.Down] = "IsDown";
            DirectionToAnimatorParam[DirectionEnum.Right] = "IsRight";
        }

        /// <summary>
        /// How fast the characters can move
        /// </summary>
        public const float MovementSpeed = 3.0f;

        /// <summary>
        /// Size of the grid
        /// </summary>
        public const float GridSize = 0.64f;

        /// <summary>
        /// A dictionary of directionEnum => Animator parameter name
        /// </summary>
        public static Dictionary<DirectionEnum, string> DirectionToAnimatorParam;

        /// <summary>
        /// How far away can the player pick up/chop/etc
        /// </summary>
        public const float TargetRange = 0.60f;
    }
}
