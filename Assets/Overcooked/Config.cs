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
    using UnityEngine;

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

            DirectionToVector = new Dictionary<DirectionEnum, Vector3>();
            DirectionToVector[DirectionEnum.Up] = Vector3.up;
            DirectionToVector[DirectionEnum.Left] = Vector3.left;
            DirectionToVector[DirectionEnum.Down] = Vector3.down;
            DirectionToVector[DirectionEnum.Right] = Vector3.right;
        }

        /// <summary>
        /// How fast the characters can move
        /// </summary>
        public const float MovementSpeed = 3.0f;

        /// <summary>
        /// Size of the grid
        /// </summary>
        public const float GridSize = 0.72f;

        /// <summary>
        /// A dictionary of directionEnum => Animator parameter name
        /// </summary>
        public static Dictionary<DirectionEnum, string> DirectionToAnimatorParam;

        /// <summary>
        /// A dictionary of directionEnum => Vector3 as direction
        /// </summary>
        public static Dictionary<DirectionEnum, Vector3> DirectionToVector;

        /// <summary>
        /// How far away can the player pick up/chop/etc
        /// </summary>
        public const float TargetRange = 0.60f;
    }
}
