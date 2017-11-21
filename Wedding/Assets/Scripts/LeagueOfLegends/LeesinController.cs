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
    public class LeesinController : BurneyController
    {
        /// <summary>
        /// Prefab for the Q projectile
        /// </summary>
        public GameObject QProjectilePrefab;

        /// <summary>
        /// The target
        /// </summary>
        protected GameObject _target;

        /// <summary>
        /// If leesin landed the Q
        /// </summary>
        protected bool _didQLand;

        /// <summary>
        /// How much time left until q is usable again
        /// </summary>
        protected float _qCooldown;

        /// <summary>
        /// How much time left until w is usable again
        /// </summary>
        protected float _wCooldown;

        /// <summary>
        /// How much time left until e is usable again
        /// </summary>
        protected float _eCooldown;

        /// <summary>
        /// How much time left until r is usable again
        /// </summary>
        protected float _rCooldown;


        /// <summary>
        /// Try to use Q
        /// </summary>
        protected void OnPressQ()
        {
            
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            if (Input.GetButtonDown(this._inputNames.AButton))
            {
                this.OnPressQ();
            }
        }
    }
}
