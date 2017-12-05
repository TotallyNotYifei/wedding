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
        public Projectile WProjectile;
        public Projectile EProjectile;

        #endregion
        /// <summary>
        /// LeeSin
        /// </summary>
        public LeesinController LeeSin;

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
            if (!this._isFacingRight)
            {
                newQProj.GetComponent<Projectile>().Velocity *= -1;
            }
        }

        protected override void Start()
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), this.LeeSin.GetComponent<BoxCollider2D>());
            base.Start();
        }

        protected override void Update()
        {
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

            this._animator.SetBool("IsMoving", stickX != 0);

            this.transform.position += new Vector3(stickX * this.BaseSpeed * Time.deltaTime, 0);

            base.Update();
        }
    }
}
