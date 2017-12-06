//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Ingredient.cs">
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

    /// <summary>
    /// Controls Lux  
    /// </summary>
    public class LuxController : Character
    {
        #region Unity Editor links
        /// <summary>
        /// Fired projectiles
        /// </summary>
        public Projectile QProjectile;
        public LuxWProjectile WProjectile;
        public LuxEProjectile EProjectile;

        /// <summary>
        /// LeeSin
        /// </summary>
        public LeesinController LeeSin;
        #endregion

        /// <summary>
        /// The fired E projectile
        /// </summary>
        private LuxEProjectile _firedEProjectile;

        /// <summary>
        /// When the character presses Q
        /// </summary>
        private void OnPressQ()
        {
            if (this.HasEffect(EffectEnum.QCoolDown))
            {
                return;
            }

            var newQProj = Instantiate(this.QProjectile);
            newQProj.transform.position = this.transform.position;
            this.ApplyEffect(EffectEnum.QCoolDown, 3.0f);
            this._animator.SetBool("HitQE", true);

            if (!this._isFacingRight)
            {
                newQProj.GetComponent<Projectile>().Velocity *= -1;
            }
        }

        /// <summary>
        /// Called when the user presses W
        /// </summary>
        private void OnPressW()
        {
            if (this.HasEffect(EffectEnum.WCoolDown))
            {
                return;
            }

            var newWProj = Instantiate(this.WProjectile).GetComponent<LuxWProjectile>();
            newWProj.Lux = this;
            if (!this._isFacingRight)
            {
                newWProj.Velocity *= -1;
            }
            newWProj.transform.position = this.transform.position;
            this.ApplyEffect(EffectEnum.WCoolDown, Config.Lux.WCoolDown);
            this._animator.SetBool("HitW", true);

            this.ApplyEffect(EffectEnum.Snare, 0.4f);
        }

        /// <summary>
        /// Called when the player presses E
        /// </summary>
        private void OnPressE()
        {
            if (this._firedEProjectile != null)
            {
                this._firedEProjectile.Detonate();
                this._firedEProjectile = null;
            }

            else if (!this.HasEffect(EffectEnum.ECoolDown))
            {
                var newEProj = Instantiate(this.EProjectile).GetComponent<LuxEProjectile>(); ;
                newEProj.transform.position = this.transform.position;
                if (!this._isFacingRight)
                {
                    newEProj.Velocity *= -1;
                }
                this.ApplyEffect(EffectEnum.ECoolDown, Config.Lux.ECoolDown);
                this.ApplyEffect(EffectEnum.Snare, 0.4f);
                this._animator.SetBool("HitQE", true);

                this._firedEProjectile = newEProj;
            }
        }

        /// <summary>
        /// Called when the player presses R
        /// </summary>
        private void OnPressR()
        {
            this._animator.SetBool("HitR", true);
            this.ApplyEffect(EffectEnum.Snare, 1.2f);
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), this.LeeSin.GetComponent<BoxCollider2D>());
            base.Start();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            this._animator.SetBool("HitQE", false);
            this._animator.SetBool("HitW", false);
            this._animator.SetBool("HitR", false);

            var stickX = 0;
            if (Input.GetKey(KeyCode.J))
            {
                stickX = -1;
                this._sprite.flipX = true;
                this._isFacingRight = false;
            }
            else if (Input.GetKey(KeyCode.L))
            {
                stickX = 1;
                this._sprite.flipX = false;
                this._isFacingRight = true;
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                this.OnPressQ();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                this.OnPressW();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                this.OnPressE();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                this.OnPressR();
            }

            this._animator.SetBool("IsMoving", stickX != 0);

            this.transform.position += new Vector3(stickX * this.BaseSpeed * Time.deltaTime, 0);

            base.Update();
        }
    }
}
