//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GameController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Controls the overall flow of the game
    /// </summary>
    public class GameController : MonoBehaviour
    {
        /// <summary>
        /// Gets the current instance of the <see cref="GameController"/> class
        /// </summary>
        public static GameController CurrentInstacne { get; private set; }

        /// <summary>
        /// Used as initialization
        /// </summary>
        protected void Start()
        {
            GameController.CurrentInstacne = this;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void FixedUpdate()
        {
          
        }
    }
}
