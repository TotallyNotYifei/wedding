//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Character.cs">
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
    /// A base class for all  league of legends characters
    /// </summary>
    public class Character :  MonoBehaviour
    {
        /// <summary>
        /// The base unit moved per second
        /// </summary>
        public float BaseSpeed;

        /// <summary>
        /// A dictionary of active effects =>  duration left
        /// </summary>
        public IDictionary<EffectEnum, float> Effects { get; private set; }


        /// <summary>
        /// The 2D rigidbody
        /// </summary>
        protected Rigidbody2D _rgbd;

        /// <summary>
        /// Adds a debuff
        /// </summary>
        /// <param name="effect">Target debuff</param>
        /// <param name="duration">How much total duration for the debuff</param>
        public void ApplyEffect(EffectEnum effect, float duration)
        {
            float existingEffect;
            if (this.Effects.TryGetValue(effect, out existingEffect))
            {
                if (existingEffect > duration)
                {
                    return;
                }
            }

            this.Effects[effect] = duration;
        }

        /// <summary>
        /// If the chracter has active target effect
        /// </summary>
        /// <param name="effect">Target effect</param>
        /// <returns>True if the active effect is in effect</returns>
        public bool HasEffect(EffectEnum effect)
        {
            return this.Effects.ContainsKey(effect);
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected virtual void Start()
        {
            this.Effects = new Dictionary<EffectEnum, float>();
            this._rgbd = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected virtual void Update()
        {
            var keys = this.Effects.Keys.ToList();
            for (int i = keys.Count-1;i>=0;i--)
            {
                var key = keys[i];
                if (Effects[key] < Time.deltaTime)
                {
                    this.Effects.Remove(key);
                }
                else
                {
                    Effects[key] -= Time.deltaTime;
                }
            }
        }
    }
}
