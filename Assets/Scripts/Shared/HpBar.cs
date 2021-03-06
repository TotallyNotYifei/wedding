﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HpBar.cs">
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
    /// The HP bar
    /// </summary>
    public class HpBar : MonoBehaviour
    {
        /// <summary>
        /// Game object for the actual hp bar
        /// </summary>
        public GameObject BarObject;

        /// <summary>
        /// Sets a new ratio to the HP bar
        /// </summary>
        /// <param name="newRatio">New ratio</param>
        public void setRatio(float newRatio)
        {
            this.BarObject.transform.localScale = new Vector3(newRatio, 1, 1);
            this.BarObject.transform.localPosition = new Vector3(-0.5f + newRatio / 2, 0, 0);
        }
    }
}
