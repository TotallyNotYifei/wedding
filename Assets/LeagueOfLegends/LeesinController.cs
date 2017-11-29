//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LeesinController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Scripts.Shared;

    /// <summary>
    /// Controls Lee Sin
    /// </summary>
    public class LeesinController : Character
    {
        /// <summary>
        /// Prefab for the Q projectile
        /// </summary>
        public Projectile QProjectilePrefab;

        /// <summary>
        /// Speed of the resonating strike
        /// </summary>
        public float ResonatingStrikeSpeed;

        /// <summary>
        /// The target
        /// </summary>
        private GameObject _Qtarget;

        /// <summary>
        /// If LeeSin si in the middle of resonating strike
        /// </summary>
        private bool _isResonating;

        /// <summary>
        /// If Lee Sin is facing right
        /// </summary>
        private bool _isFacingRight;

        /// <summary>
        /// The sprite renderer
        /// </summary>
        private SpriteRenderer _sprite;

        /// <summary>
        /// The animator component
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// The control schema
        /// </summary>
        private InputNames _controls;

        /// <summary>
        /// When the Q landed
        /// </summary>
        public void OnQLanded(EnemyController enemy)
        {
            this._Qtarget = enemy.gameObject;
            this._animator.SetBool("Resonating", true);
        }

        /// <summary>
        /// Try to use Q
        /// </summary>
        private void OnPressQ()
        {
            // If there's a Q target, 
            if (this._Qtarget != null)
            {
                this._isResonating = true;
            }
            else if (!this.HasEffect(EffectEnum.QCoolDown))
            {
                var newQ = Instantiate(this.QProjectilePrefab.gameObject).GetComponent<Projectile>();
                newQ.transform.position = this.transform.position;
                newQ.Velocity = this._isFacingRight ? 15 : -15;
                newQ.Duration = 0.5f;
                newQ.CarriedEffect = EffectEnum.LeeQLanded;
                this._animator.SetBool("HitQ", true);
                this.ApplyEffect(EffectEnum.Snare, 0.2f);
                this.ApplyEffect(EffectEnum.QCoolDown, 3.0f);
            }
        }


        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            this._sprite = this.GetComponent<SpriteRenderer>();
            this._controls = BurneyController.ControlSchema;
            this._animator = this.GetComponent<Animator>();
            this._isFacingRight = true;

            base.Start();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            this._animator.SetBool("HitQ", false);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                this.OnPressQ();
            }

            // Check if resonating strike is happening
            if (this._Qtarget != null && this._isResonating)
            {
                var movementThisFrame = this.ResonatingStrikeSpeed * Time.deltaTime;
                var xDiff = this._Qtarget.transform.position.x - this.transform.position.x;
                if (Math.Abs(xDiff) < movementThisFrame)
                {
                    this.transform.position = this._Qtarget.transform.position;
                    this._Qtarget = null;
                    this._isResonating = false;
                    this._animator.SetBool("Resonating", false);
                    this.ApplyEffect(EffectEnum.QCoolDown, 3.0f);
                }
            }
            // Move the character based on the input
            float stickX = 0;

            if (Input.GetKey(KeyCode.A))
            {
                stickX = -1;
                this._isFacingRight = false;
                this._sprite.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                stickX = 1;
                this._isFacingRight = true;
                this._sprite.flipX = false;
            }

            if (stickX != 0 && !this.Effects.ContainsKey(EffectEnum.Snare))
            {
                this._animator.SetBool("IsMoving", true);
                this.transform.position += new Vector3(this.BaseSpeed * Time.deltaTime * Math.Sign(stickX), 0, 0);
            }
            else
            {
                this._animator.SetBool("IsMoving", false);
            }

            base.Update();
        }
    }
}
