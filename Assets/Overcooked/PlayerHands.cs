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

            return true;
        }

        /// <summary>
        /// Called when the player turns
        /// </summary>
        /// <param name="newDirection">The direction that the player is now facing</param>
        public void OnTurn(DirectionEnum newDirection)
        {
        }

        /// <summary>
        /// When the player hits E 
        /// </summary>
        /// <param name="nearest">The nearest map object</param>
        public void Interact(OvercookedMapObject nearest)
        {

        }
    }
}
