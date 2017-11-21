//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ProgressData.cs">
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
    /// Used to keep track of progress within the game. Reset upon game reset
    /// </summary>
    public class ProgressData : MonoBehaviour
    {
        /// <summary>
        /// Gets the current instance
        /// </summary>
        public static ProgressData CurrentInstance
        {
            get
            {
                if (ProgressData._currentInstance == null)
                {
                    ProgressData._currentInstance = GameObject.FindObjectOfType<ProgressData>();
                }

                return ProgressData._currentInstance;
            }
        }

        /// <summary>
        /// The current instance 
        /// </summary>
        private static ProgressData _currentInstance;

        /// <summary>
        /// How many levels has been beat
        /// </summary>
        public int LevelProgress;
    }
}
