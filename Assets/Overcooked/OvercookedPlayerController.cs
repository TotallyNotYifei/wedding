//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="OvercookedPlayerController.cs">
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
    /// Describes a controller for a player
    /// </summary>
    public class OvercookedPlayerController : MonoBehaviour
    {
        /// <summary>
        /// The direction that the player is currently facing
        /// </summary>
        public DirectionEnum CurrentlyFacing { get; private set; }

        /// <summary>
        /// THe object that the player is currently holding
        /// </summary>
        public PlayerHands Hands;

        /// <summary>
        /// The animator component
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// The rigidbody component
        /// </summary>
        private Rigidbody2D _rgbd;

        /// <summary>
        /// The renderer
        /// </summary>
        private SpriteRenderer _renderer;

        /// <summary>
        /// What's being chopped on right now
        /// </summary>
        private ChoppingBoard _currentlyChopping;

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._animator = this.GetComponent<Animator>();
            this._renderer = this.GetComponent<SpriteRenderer>();
            this._rgbd = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Called once per frame with a set offset
        /// </summary>
        protected void FixedUpdate()
        {
            #region Handles Movement
            if (this._currentlyChopping != null)
            {
                return;
            }

            var stickX = 0;
            var stickY = 0;

            if (Input.GetKey(KeyCode.W))
            {
                stickY = 1;
                this.CurrentlyFacing = DirectionEnum.Up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                stickY = -1;
                this.CurrentlyFacing = DirectionEnum.Down;
            }

            if (Input.GetKey(KeyCode.D))
            {
                stickX = 1;
                this.CurrentlyFacing = DirectionEnum.Right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                stickX = -1;
                this.CurrentlyFacing = DirectionEnum.Left;
            }

            if (stickX != 0 || stickY != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    var direction = (DirectionEnum)i;
                    this._animator.SetBool(Config.DirectionToAnimatorParam[direction], direction == this.CurrentlyFacing);
                }

            }

            var movementThisFrame = new Vector3(stickX, stickY).normalized * Config.MovementSpeed * Time.deltaTime;
            this._rgbd.MovePosition(this.transform.position + movementThisFrame);
            this.Hands.UpdateHands(this.CurrentlyFacing);
            #endregion
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            #region Handles chopping
            if (Input.GetKeyUp(KeyCode.Q) || (this._currentlyChopping != null && !this._currentlyChopping.CanChop()))
            {
                if (this._currentlyChopping != null)
                {
                    this._currentlyChopping.IsChopping = false;
                }
                this._currentlyChopping = null;
                this._animator.SetBool("IsChopping", false);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                var checkPos = this.transform.position + Config.HoldingDirectionOffset[this.CurrentlyFacing];
                var closestMapObject = OvercookedGameController.CurrentInstance.GetClosestMapObjectAtWorldPosition(
                    checkPos,
                    Config.TargetRange
                );
                if (closestMapObject is ChoppingBoard)
                {
                    var choppingBoard = closestMapObject.GetComponent<ChoppingBoard>();

                    if (choppingBoard.CanChop())
                    {
                        this._currentlyChopping = choppingBoard;
                        this._currentlyChopping.IsChopping = true;
                        this._animator.SetBool("IsChopping", true);
                    }
                }
            }
            #endregion

            #region Handles grabbing/dropping item
            if (Input.GetKeyDown(KeyCode.E))
            {
                var checkPos = this.transform.position + Config.HoldingDirectionOffset[this.CurrentlyFacing];
                var closestMapObject = OvercookedGameController.CurrentInstance.GetClosestMapObjectAtWorldPosition(
                    checkPos,
                    Config.TargetRange
                );

                if (closestMapObject == null)
                {
                    return;
                }

                IContainer source = this.Hands;
                IContainer destination = closestMapObject;

                if (source.IsEmpty && destination.IsEmpty)
                {
                    return;
                }

                var sourceHolding = source.Peek() as IContainer;
                var destinationHolding = destination.Peek() as IContainer;

                if (destinationHolding != null && sourceHolding != null)
                {
                    source = sourceHolding;
                    destination = destinationHolding;
                }

                else if (source.IsEmpty)
                {
                    var swapValue = source;
                    source = destination;
                    destination = swapValue;
                }

                TransferItem(source, destination);
            }
            #endregion
        }

        /// <summary>
        /// Transfer item from the source to destination
        /// </summary>
        /// <param name="source">source container</param>
        /// <param name="destination">destination container</param>
        private static void TransferItem(IContainer source, IContainer destination)
        {
            var content = source.Peek();

            if (!destination.TryAdd(content))
            {
                return;
            }

            source.RetrieveContent();
        }
    }
}
