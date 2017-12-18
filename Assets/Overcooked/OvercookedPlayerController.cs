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

            #region Handles Chopping
            // Can't chop while holding something
            this._animator.SetBool("IsChopping", Input.GetKey(KeyCode.Q));
            #endregion
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            #region Handles grabbing/dropping item
            if (Input.GetKeyDown(KeyCode.E))
            {
                var checkPos = this.transform.position + Config.FaceDirectionOffset[this.CurrentlyFacing];
                var closestMapObject = OvercookedGameController.CurrentInstance.GetClosestMapObjectAtWorldPosition(
                    checkPos,
                    Config.TargetRange
                );
                if (this.CurrentlyHolding != null)
                {
                    this.TryPlaceItem(closestMapObject);
                }
                else
                {
                    this.TryGrabItem(closestMapObject);
                }
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
            if (closestObject.TryTakeItem(out newObject))
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
            if (closestObject == null)
            {
                return;
            }

            if (closestObject.TryPlaceItem(this.CurrentlyHolding))
            {
                this.CurrentlyHolding = null;
            }
        }
    }
}
