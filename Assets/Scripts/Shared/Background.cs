//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Background.cs">
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
    /// Describes the game background
    /// </summary>
    public class Background : MonoBehaviour
    {
        /// <summary>
        /// The background's movement scale compared to the camera
        /// </summary>
        public float MovementScale;

        /// <summary>
        /// The main camera
        /// </summary>
        public GameObject MainCamera;

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            this.transform.position = this.MainCamera.transform.position * this.MovementScale;
        }
    }
}
