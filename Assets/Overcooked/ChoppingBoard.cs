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
    public class ChoppingBoard : KitchenCounter
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
        /// If the board is currently chopping
        /// </summary>
        public bool IsChopping { get; set; }
        
        /// <summary>
        /// How much is chopped, from a scale of 0 to 1 with 1 being done chopping
        /// </summary>
        private float _chopProgress;

        /// <summary>
        /// The currently placed ingredient
        /// </summary>
        private Ingredient _currentPlacedIngredient;

        /// <summary>
        /// A list of choppable ingredients
        /// </summary>
        private static HashSet<IngredientEnum> Choppable = new HashSet<IngredientEnum>() {
            IngredientEnum.Lettunce,
            IngredientEnum.Onion,
            IngredientEnum.RawMeat,
            IngredientEnum.Tomato };

        /// <summary>
        /// Try to place an item
        /// </summary>
        /// <param name="item">New item to be placed</param>
        /// <returns>True if operation succeed</returns>
        public override bool TryAdd(IHoldable item)
        {
            if (!(item is Ingredient))
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

            this.CurrentlyPlaced = ingredient;
            this._currentPlacedIngredient = ingredient;
            this._chopProgress = 0;
            ingredient.transform.position = this.transform.position + new UnityEngine.Vector3(0, Config.ItemPlacementHeight);
            this.ProgressBar.gameObject.SetActive(true);

            return true;
        }

        public override IHoldable Peek()
        {
            if (this.CanChop())
            {
                return null;
            }

            return base.Peek();
        }

        public override IHoldable RetrieveContent()
        {
            if (this.CanChop())
            {
                return null;
            }

            var result = base.RetrieveContent();
            if (result != null)
            {
                this._currentPlacedIngredient = null;
            }

            return result;
        }

        /// <summary>
        /// If the board can chop things right now
        /// </summary>
        /// <returns>True if the board can chop things</returns>
        public bool CanChop()
        {
            return this.CurrentlyPlaced != null && this._chopProgress < 1;
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
                    this._currentPlacedIngredient.FinishChopping();
                }
            }
        }
    }
}
