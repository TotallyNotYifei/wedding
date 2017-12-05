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
        /// <summary>
        /// LeeSin
        /// </summary>
        public LeesinController LeeSin;

        /// <summary>
        /// If the character is facing to the right
        /// </summary>
        private bool _isFacingRight;

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

            this._animator.SetBool("IsMoving", stickX != 0);

            this.transform.position += new Vector3(stickX * this.BaseSpeed * Time.deltaTime, 0);

            base.Update();
        }
    }
}
