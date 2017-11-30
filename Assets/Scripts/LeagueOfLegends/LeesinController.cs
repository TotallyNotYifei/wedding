//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LeesinController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Shared;

    /// <summary>
    /// Controls Lee Sin
    /// </summary>
    public class LeesinController : Character
    {
        /// <summary>
        /// Prefab for the Q projectile
        /// </summary>
        public GameObject QProjectilePrefab;

        /// <summary>
        /// The target
        /// </summary>
        private GameObject _target;

        /// <summary>
        /// If LeeSin landed the Q
        /// </summary>
        private bool _didQLand;

        /// <summary>
        /// How much time left until q is usable again
        /// </summary>
        private float _qCooldown;

        /// <summary>
        /// How much time left until w is usable again
        /// </summary>
        private float _wCooldown;

        /// <summary>
        /// How much time left until e is usable again
        /// </summary>
        private float _eCooldown;

        /// <summary>
        /// How much time left until r is usable again
        /// </summary>
        private float _rCooldown;

        /// <summary>
        /// The sprite renderer
        /// </summary>
        private SpriteRenderer _sprite;

        /// <summary>
        /// The control schema
        /// </summary>
        private InputNames _controls;

        /// <summary>
        /// Try to use Q
        /// </summary>
        private void OnPressQ()
        {
            
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._sprite = this.GetComponent<SpriteRenderer>();
            this._controls = BurneyController.ControlSchema;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            if (Input.GetButtonDown(this._controls.AButton))
            {
                this.OnPressQ();
            }

            // Move the character based on the input
            var stickX = Input.GetAxis(this._controls.XAxis);
        }
    }
}
