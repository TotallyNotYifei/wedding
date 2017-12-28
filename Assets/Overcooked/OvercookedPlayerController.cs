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
    public  class OvercookedPlayerController : MonoBehaviour
    {
        /// <summary>
        /// The direction that the player is currently facing
        /// </summary>
        public DirectionEnum CurrentlyFacing { get; private set; }

        /// <summary>
        /// Game object for the arms
        /// </summary>
        public GameObject Arms;

        /// <summary>
        /// THe object that the player is currently holding
        /// </summary>
        public Holdable CurrentlyHolding;

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
        /// Renderer of the item currently being held
        /// </summary>
        private SpriteRenderer _holdingRenderer;

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
            this.CurrentlyHolding = null;
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

            if(this.CurrentlyHolding != null)
            {
                this.CurrentlyHolding.transform.position = this.transform.position + Config.HoldItemOffset[this.CurrentlyFacing];
                this._holdingRenderer.sortingOrder = Config.HoldItemLayer[this.CurrentlyFacing];
            }
            #endregion
        }

        /// <summary>
        /// handles item transfer between the play and map objects
        /// </summary>
        private void HandleItemTransfer()
        {
            var checkPos = this.transform.position + Config.FaceDirectionOffset[this.CurrentlyFacing];
            var closestMapObject = OvercookedGameController.CurrentInstance.GetClosestMapObjectAtWorldPosition(
                checkPos,
                Config.TargetRange
            );

            if (closestMapObject == null)
            {
                return;
            }

            // If nothing is held, grab directly off of the map object
            if (this.CurrentlyHolding == null)
            {
                this.TryGrabItem(closestMapObject);
            }
            // If the player is holding ingredient, deposit
            else
            {
                this.TryPlaceItem(closestMapObject);
            }
            //else if (this.CurrentlyHolding is Container)
            //{
            //    var holdingContainer = this.CurrentlyHolding as Container;

            //    // If holding an empty plate, try to retrieve first
            //    if (holdingContainer is Plate && holdingContainer.IsEmpty)
            //    {
            //        Ingredient resultIngredient;
            //        if (closestMapObject.TryTakeItemWithPlate(holdingContainer as Plate, out resultIngredient))
            //        {
            //            holdingContainer.TryAddIngredient(resultIngredient);
            //        }
            //        else
            //        {
            //            if (closestMapObject.TryPlaceItem(this.CurrentlyHolding))
            //            {
            //                this.CurrentlyHolding = null;
            //            }
            //        }
            //    }
            //    // Check what's being placed
            //    else
            //    {
            //        if (closestMapObject.TryPlaceItem(this.CurrentlyHolding))
            //        {
            //            this.CurrentlyHolding = null;
            //        }
            //    }
            //}
            //else
            //{
            //    if (closestMapObject.TryPlaceItem(this.CurrentlyHolding))
            //    {
            //        this.CurrentlyHolding = null;
            //    }
            //}
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
                var checkPos = this.transform.position + Config.FaceDirectionOffset[this.CurrentlyFacing];
                var closestMapObject = OvercookedGameController.CurrentInstance.GetClosestMapObjectAtWorldPosition(
                    checkPos,
                    Config.TargetRange
                );
                if (closestMapObject.ObjectType == OvercookedMapObjectTypes.ChoppingBoard)
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
                this.HandleItemTransfer();
            }
            #endregion
        }

        /// <summary>
        /// Called when the player tries to grab an item
        /// </summary>
        /// <param name="closestObject">The closet map object</param>
        private void TryGrabItem(OvercookedMapObject closestObject)
        {
            if (closestObject == null)
            {
                return;
            }

            Holdable newObject = null;
            if (closestObject.TryTakeItemWithHand(out newObject))
            {
                this.CurrentlyHolding = newObject;
                this._holdingRenderer = newObject.GetComponent<SpriteRenderer>();
            }
        }

        /// <summary>
        /// Called when the player tries to place an item
        /// </summary>
        /// <param name="closestObject">The closest map object</param>
        private void TryPlaceItem(OvercookedMapObject closestObject)
        {
            // If there's nothing to interact with, do nothing
            if (closestObject == null)
            {
                return;
            }

            // If the map object can take the item, do it normally
            if (closestObject.TryPlaceItem(this.CurrentlyHolding))
            {
                this.CurrentlyHolding = null;
                return;
            }

            // If player is holding a plate
            if (this.CurrentlyHolding.HoldableType == HoldableTypes.Plate)
            {
                var holdingPlate = this.CurrentlyHolding as Plate;

                // If the plate is empty, try to grab the ingredient
                if (holdingPlate.Ingredeints.Count == 0)
                {
                    Ingredient result;
                    if (closestObject.TryTakeItemWithPlate(holdingPlate, out result))
                    {
                        holdingPlate.TryAddIngredient(result);
                    }

                    return;
                }

                // If the plate contains only one ingredient, try pushing that ingredient
                if (holdingPlate.Ingredeints.Count == 1)
                {
                    var holdingPlateContent = holdingPlate.Ingredeints[0];
                    if (closestObject.TryPlaceItem(holdingPlateContent))
                    {
                        holdingPlate.Ingredeints.RemoveAt(0);
                        return;
                    }
                }
            }
        }
    }
}
