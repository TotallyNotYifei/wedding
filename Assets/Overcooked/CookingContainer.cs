//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CookingContainer.cs">
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
    /// Defines a container action
    /// </summary>
    public abstract class CookingContainer : HoldableContainer
    {
        /// <summary>
        /// The progress bar
        /// </summary>
        public HpBar ProgressBar;

        /// <summary>
        /// How long it takes to cook over all
        /// </summary>
        public float TotalTimeToCook;

        /// <summary>
        /// How long it takes for the food to burn
        /// </summary>
        public float BurnTime;

        /// <summary>
        /// How much cooking can be done
        /// </summary>
        public float CookProgress;

        /// <summary>
        /// How far the cooking can be done based on the ingredients put in
        /// </summary>
        public float ProgressLimit;

        /// <summary>
        /// How long this dish has until it burns
        /// </summary>
        protected float TimeTillBurn;

        /// <summary>
        /// If the cooking container is on a burner
        /// </summary>
        public bool IsOnBurner
        {
            get
            {
                return this._isOnBurner;
            }
            set
            {
                this._isOnBurner = value;
                if (this.Ingredients.Count > 0)
                {
                    this.ProgressBar.gameObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// If the cooking container is on a burner
        /// </summary>
        private bool _isOnBurner;

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            this.CookProgress = 0;
            this.ResetTimeTillBurn();
            base.Start();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override  void Update()
        {           
            if (this.IsOnBurner)
            {
                if (this.CookProgress >= this.ProgressLimit)
                {
                    this.TimeTillBurn -= Time.deltaTime;
                }
                else
                {
                    this.CookProgress += 1.0f / this.TotalTimeToCook * Time.deltaTime;
                    if (this.CookProgress >= 1)
                    {
                        this.OnFinishCookingHook();
                    }
                }
            }

            if (this.Ingredients.Count == 0 || this.CookProgress >= 1)
            {
                this.ProgressBar.gameObject.SetActive(false);
            }
            else
            {
                this.ProgressBar.setRatio(this.CookProgress);
            }

            base.Update();
        }

        /// <summary>
        /// Resets how long it'll take to burn
        /// </summary>
        protected void ResetTimeTillBurn()
        {
            this.TimeTillBurn = BurnTime;
        }

        /// <summary>
        /// Called when the cooking is finished
        /// </summary>
        protected virtual void OnFinishCookingHook()
        {
            // Do nothing by default
        }

        public override bool IsEmpty
        {
            get
            {
                return this.Ingredients.Count > 0;
            }
        }

        public override void Dump()
        {
            this.CookProgress = 0;
            this.ProgressBar.setRatio(0);
            this.ProgressBar.gameObject.SetActive(false);
            this.ProgressLimit = 0;
            base.Dump();
        }
    }
}
