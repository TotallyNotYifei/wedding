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
        public List<Plate> plates;

        /// <summary>
        /// How many of each item must be delivered before the game is over
        /// </summary>
        public int RequiredBurgerCount;
        public int RequiredSoupCount;
        public int RequiredSaladCount;

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
            if (this.plates.Count > 0)
            {
                newObject.TryPlaceItem(this.plates[0]);
                this.plates.RemoveAt(0);
            }
        }


        /// Gets the closest map object at the given position
        /// </summary>
        /// <param name="position">Target position, origin of the search circle</param>
        /// <param name="range">Max distance of an item to be considered in range</param>
        public OvercookedMapObject GetClosestMapObjectAtWorldPosition(Vector3 position, float range)
        {
            float? minDistance = null;
            OvercookedMapObject result = null;
            foreach(var obj in this._mapObjects)
            {
                var distance = (obj.transform.position - position).magnitude;
                if (distance <= range)
                {
                    if (minDistance == null || minDistance > distance)
                    {
                        minDistance = distance;
                        result = obj;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// used for initialization
        /// </summary>
        protected void Start()
        {

        }

        /// <summary>
        /// Check if a plate is finished according to a recipe
        /// </summary>
        /// <param name="plate">Target plate</param>
        /// <param name="receipe">Target recipe</param>
        /// <returns></returns>
        private bool IsPlateComplete(Plate plate, List<Ingredient> recipe)
        {
            foreach (var item in recipe)
            {
                if (!plate.Ingredeints.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
