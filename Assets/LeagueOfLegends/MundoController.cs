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
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            // Move if not snared
            if (!this.HasEffect(EffectEnum.Snare))
            {
                var movementThisFrame = this.BaseSpeed * Time.deltaTime;
                if (this.Effects.ContainsKey(EffectEnum.Slow))
                {
                    movementThisFrame /= 2;
                }

                this.transform.position += new Vector3(movementThisFrame, 0);
            }

            base.Update();
        }

        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log("BANG2");
        }


        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
