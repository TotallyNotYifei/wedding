//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Map.cs">
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
    /// Controls the overall map behavior
    /// </summary>
    public class OvercookedGameController : MonoBehaviour
    {
        /// <summary>
        /// Gets the current instance of the <see cref="OvercookedGameController"/> class
        /// </summary>
        public static OvercookedGameController CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    _currentInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<OvercookedGameController>();
                }

                return _currentInstance;
            }
        }

        /// <summary>
        /// The current instance of the Map object
        /// </summary>
        private static OvercookedGameController _currentInstance;

        /// <summary>
        /// All map objects
        /// </summary>
        private IList<OvercookedMapObject> _mapObjects = new List<OvercookedMapObject>();

        /// <summary>
        /// Adds a new map object
        /// </summary>
        /// <param name="newObject">New object to be added</param>
        public void AddMapObject(OvercookedMapObject newObject)
        {
            this._mapObjects.Add(newObject);
        }

        /// <summary>
        /// used for initialization
        /// </summary>
        protected void Start()
        {

        }
    }
}
