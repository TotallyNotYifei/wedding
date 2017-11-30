//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MundoController.cs">
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
    /// Controls the NPC Mundo
    /// </summary>
    public class MundoController : EnemyController
    {
        /// <summary>
        /// The sprite rendere
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            this._animator = this.GetComponent<Animator>();

            base.Start();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            var movementThisFrame = this.BaseSpeed * Time.deltaTime;

            if (this.HasEffect(EffectEnum.Knockback))
            {
                movementThisFrame *= -2.3f;
                //this._animator.SetBool("KnockBack", true);
            }
            else if (this.HasEffect(EffectEnum.Knockforward))
            {
                movementThisFrame *= 2.3f;
                //this._animator.SetBool("KnockBack", false);
            }
            else if (this.HasEffect(EffectEnum.Snare))
            {
                movementThisFrame = 0;
            }
            else if (this.HasEffect(EffectEnum.Slow))
            {
                movementThisFrame /= 2;
            }

            this.transform.position += new Vector3(movementThisFrame, 0);

            base.Update();
        }
    }
}
