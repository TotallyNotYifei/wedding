//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerHands.cs">
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
    /// Describes the player's hands
    /// </summary>
    public class PlayerHands : MonoBehaviour, IContainer
    {
        /// <summary>
        /// The item that's being currently held
        /// </summary>
        public IHoldable CurrentlyHolding { get; private set; }

        private MonoBehaviour CurrentlyHeldObject
        {
            get
            {
                return this.CurrentlyHolding as MonoBehaviour;
            }
        }

        private SpriteRenderer _sprite;

        public bool IsEmpty
        {
            get
            {
                return this.CurrentlyHolding == null;
            }
        }

        public IHoldable Peek()
        {
            return this.CurrentlyHolding;
        }

        public IHoldable RetrieveContent()
        {
            var result = this.CurrentlyHolding;
            this.CurrentlyHolding = null;
            return result;
        }

        public bool TryAdd(IHoldable newItem)
        {
            if (this.CurrentlyHolding != null)
            {
                return false;
            }

            this.CurrentlyHolding = newItem;
            return true;
        }

        /// <summary>
        /// Used for intialization
        /// </summary>
        protected void Start()
        {
            this._sprite = this.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Called when the player turns
        /// </summary>
        /// <param name="currentlyfacing">The direction that the player is now facing</param>
        public void UpdateHands(DirectionEnum currentlyfacing)
        {
            var newLayer = currentlyfacing == DirectionEnum.Up ? -2 : 0;

            this._sprite.sortingOrder = newLayer;
            if (this.CurrentlyHolding != null)
            {
                this.CurrentlyHolding.SetDisplayLayer(newLayer + 1);
                this.CurrentlyHeldObject.transform.position = this.transform.position + Config.HoldingDirectionOffset[currentlyfacing];
            }
        }
    }
}
