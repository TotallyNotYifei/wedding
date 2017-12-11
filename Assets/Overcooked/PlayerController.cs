//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerController.cs">
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
    public  class PlayerController : MonoBehaviour
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
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._animator = this.GetComponent<Animator>();
            this.CurrentlyHolding = null;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            var stickX = 0;
            var stickY = 0;

            if (Input.GetKey(KeyCode.D))
            {
                stickX = 1;
                this.CurrentlyFacing = DirectionEnum.Right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                stickX = -11;
                this.CurrentlyFacing = DirectionEnum.Left;
            }

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

            for(int i =0;i<4;i++)
            {
                var direction = (DirectionEnum)i;
                this._animator.SetBool(Config.DirectionToAnimatorParam[direction], direction == this.CurrentlyFacing);
            }

            var movementThisFrame = new Vector3(stickX, stickY).normalized * Config.MovementSpeed * Time.deltaTime;
            this.transform.position += movementThisFrame;
        }
    }
}
