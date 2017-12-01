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
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The base unit moved per second
        /// </summary>
        public float BaseSpeed;

        /// <summary>
        /// Total health points
        /// </summary>
        public float TotalHP;

        /// <summary>
        /// The current HP
        /// </summary>
        public float CurrentHP { get; private set; }

        /// <summary>
        /// Prefab for a ward
        /// </summary>
        public GameObject WardPrefab;

        /// <summary>
        /// Effect for placing a ward
        /// </summary>
        public Projectile WardPlacementEffect;

        /// <summary>
        /// A dictionary of active effects =>  duration left
        /// </summary>
        public IDictionary<EffectEnum, float> Effects { get; private set; }

        /// <summary>
        /// The 2D rigidbody
        /// </summary>
        protected Rigidbody2D _rgbd;

        /// <summary>
        /// Removes the given effect
        /// </summary>
        /// <param name="effect">Target effect for removal</param>
        public void RemoveEffec(EffectEnum effect)
        {
            this.Effects.Remove(effect);
        }

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
        /// Takes damage
        /// </summary>
        /// <param name="damage">Damage taken</param>
        public void TakeDamage(float damage)
        {
            this.CurrentHP = Math.Max(this.CurrentHP - damage, 0);
            Debug.Log(this.CurrentHP);
        }

        /// <summary>
        /// Places a ward
        /// </summary>
        /// <param name="isFacingRight">if the ward should be placed to the right or left</param>
        protected void PlaceWard(bool isFacingRight)
        {
            var newWard = Instantiate(this.WardPrefab);
            var xDiff = isFacingRight ? Config.WardPlacementRange : -Config.WardPlacementRange;
            newWard.transform.position = this.transform.position + new Vector3(xDiff, 0);
            var effect = Instantiate(this.WardPlacementEffect).GetComponent<Projectile>();
            if (!isFacingRight)
            {
                effect.Velocity *= -1;
            }
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected virtual void Start()
        {
            this.Effects = new Dictionary<EffectEnum, float>();
            this._rgbd = this.GetComponent<Rigidbody2D>();
            this.CurrentHP = this.TotalHP;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected virtual void Update()
        {
            var keys = this.Effects.Keys.ToList();
            for (int i = keys.Count - 1; i >= 0; i--)
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