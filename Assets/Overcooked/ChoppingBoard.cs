//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ChoppingBoard.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overcooked
{
    using Scripts.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Describes the chopping board
    /// </summary>
    public class ChoppingBoard : OvercookedMapObject
    {
        /// <summary>
        /// The progress bar
        /// </summary>
        public HpBar ProgressBar;

        /// <summary>
        /// How long it takes to chop the item
        /// </summary>
        public float TimeToChop;

        /// <summary>
        /// Returns chopping board as type
        /// </summary>
        public override OvercookedMapObjectTypes ObjectType
        {
            get
            {
                return OvercookedMapObjectTypes.ChoppingBoard;
            }
        }

        /// <summary>
        /// If the board is currently chopping
        /// </summary>
        public bool IsChopping { get; set; }
        
        /// <summary>
        /// How much is chopped, from a scale of 0 to 1 with 1 being done chopping
        /// </summary>
        private float _chopProgress;

        /// <summary>
        /// The ingredient that's currently placed
        /// </summary>
        private Ingredient _currentlyPlaced;

        /// <summary>
        /// A list of choppable ingredients
        /// </summary>
        private HashSet<IngredientEnum> Choppable = new HashSet<IngredientEnum>() { IngredientEnum.Lettunce, IngredientEnum.Onion, IngredientEnum.RawMeat, IngredientEnum.Tomato };

        /// <summary>
        /// Try to take an item from mthe board
        /// </summary>
        /// <param name="item">Item to be taken out</param>
        /// <returns>True if successful</returns>
        public override bool TryTakeItemWithHand(out Holdable item)
        {
            // if there's nothing to take
            if (this._currentlyPlaced == null)
            {
                item = null;
                return false;
            }

            // If chopping is in progress
            if (0 < this._chopProgress && this._chopProgress< 1)
            {
                item = null;
                return false;
            }

            item = this._currentlyPlaced;
            this._currentlyPlaced = null;
            return true;
        }

        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="item">New item to be placed</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryPlaceItem(Holdable item)
        {
            if (item.GetType() != typeof(Ingredient))
            {
                return false;
            }

            var ingredient = item as Ingredient;
            if (ingredient.IsChopped)
            {
                return false;
            }

            if (!Choppable.Contains(ingredient.IngredientType))
            {
                return false;
            }

            this._currentlyPlaced = ingredient;
            this._chopProgress = 0;
            ingredient.transform.position = this.transform.position + new UnityEngine.Vector3(0, Config.ItemPlacementHeight);
            this.ProgressBar.gameObject.SetActive(true);

            return true;
        }

        /// <summary>
        /// If the board can chop things right now
        /// </summary>
        /// <returns>True if the board can chop things</returns>
        public bool CanChop()
        {
            return this._currentlyPlaced != null && this._chopProgress < 1;
        }

        /// <summary>
        /// used for initialization
        /// </summary>
        protected override void Start()
        {
            this.ProgressBar.gameObject.SetActive(false);

            base.Start();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            if (this.ProgressBar.gameObject.activeSelf)
            {
                this.ProgressBar.setRatio(this._chopProgress);
            }
            if (this.IsChopping && this._chopProgress < 1)
            {
                this._chopProgress += (1.0f / this.TimeToChop) * Time.deltaTime;
                if (this._chopProgress >= 1)
                {
                    this.ProgressBar.gameObject.SetActive(false);
                    this._currentlyPlaced.FinishChopping();
                }
            }
        }
    }
}
