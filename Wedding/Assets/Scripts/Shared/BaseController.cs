//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseController.cs">
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
    /// The  basic controller
    /// </summary>
    public class BaseController : MonoBehaviour
    {
        /// <summary>
        /// Gets the controller index
        /// </summary>
        protected virtual int _controllerIndex { get; private set; }

        /// <summary>
        /// Gets the input names
        /// </summary>
        protected InputNames _inputNames
        {
            get
            {
                return InputMapping.GetInputNames(this._controllerIndex);
            }
        }

        /// <summary>
        /// Called  once per frame
        /// </summary>
        protected virtual void Update()
        {
            
        }
    }
}
