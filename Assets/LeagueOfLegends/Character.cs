﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Character.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.LeagueOfLegends
{
    using Scripts.Shared;
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
        /// The HP bar game object
        /// </summary>
        public HpBar HpBarObject;

        /// <summary>
        /// A dictionary of active effects =>  duration left
        /// </summary>
        public IDictionary<EffectEnum, float> Effects { get; private set; }

        /// <summary>
        /// The 2D rigidbody
        /// </summary>
        protected Rigidbody2D _rgbd;

        /// <summary>
        /// The animator component
        /// </summary>
        protected Animator _animator;

        /// <summary>
        /// The sprite renderer
        /// </summary>
        protected SpriteRenderer _sprite;

        /// <summary>
        /// If the character is facing to the right
        /// </summary>
        protected bool _isFacingRight;

        /// <summary>
        /// If the unit is friendly
        /// </summary>
        protected virtual bool IsFriendly { get { return true; } }

        /// <summary>
        /// The currently active shield effect
        /// </summary>
        private GameObject _luxShieldEffect;

        /// <summary>
        /// Removes the given effect
        /// </summary>
        /// <param name="effect">Target effect for removal</param>
        public void RemoveEffect(EffectEnum effect)
        {
            this.Effects.Remove(effect);
        }

        /// <summary>
        /// Applies an effect
        /// </summary>
        /// <param name="effect">Target effect</param>
        /// <param name="duration">Duration of the effect</param>
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
        /// Adds a debuff
        /// </summary>
        /// <param name="effect">Target debuff</param>
        /// <param name="duration">How much total duration for the debuff</param>
        public void ApplyEffects(List<EffectEnum> effects, float duration)
        {
            foreach (var effect in effects)
            {
                if (effect == EffectEnum.LuxMark)
                {
                    this.ApplyEffect(effect, Config.Lux.MarkDuration);
                }
                else
                {
                    this.ApplyEffect(effect, duration);
                }
            }
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
        /// Gets the remaining duration of the effect, 0 if none
        /// </summary>
        /// <param name="e">Target effect</param>
        /// <returns>Remaining duration, 0 if unaffected</returns>
        public float GetEffectDuration(EffectEnum e)
        {
            float result;
            if (this.Effects.TryGetValue(e, out result))
            {
                return result;
            }

            return 0;
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
            //var effect = Instantiate(this.WardPlacementEffect).GetComponent<Projectile>();
            //if (!isFacingRight)
            //{
            //    effect.Velocity *= -1;
            //}
        }

        /// <summary>
        /// Fires a normal projectile
        /// </summary>
        /// <param name="projectilePrefab">Projectile prefab to be fired</param>
        /// <param name="isFacingRight">If the character is facing right</param>
        /// <param name="cooldownEffect">The cooldown </param>
        /// <param name="cooldownDuration">how long the cooldown is</param>
        /// <param name="animatorParam">Parameter for the fire animation</param>
        protected void FireNormalProjectile(Projectile projectilePrefab, bool isFacingRight, EffectEnum cooldownEffect, float cooldownDuration, string animatorParam)
        {
            if (this.HasEffect(cooldownEffect))
            {
                return;
            }

            var newQProj = Instantiate(projectilePrefab);
            newQProj.transform.position = this.transform.position;
            this.ApplyEffect(cooldownEffect, cooldownDuration);
            this._animator.SetBool(animatorParam, true);

            if (!isFacingRight)
            {
                newQProj.GetComponent<Projectile>().Velocity *= -1;
            }
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected virtual void Start()
        {
            this.Effects = new Dictionary<EffectEnum, float>();
            this._rgbd = this.GetComponent<Rigidbody2D>();
            this._animator = this.GetComponent<Animator>();
            this._sprite = this.GetComponent<SpriteRenderer>();
            this.CurrentHP = this.TotalHP;
            this._isFacingRight = true;
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

            this.HpBarObject.setRatio((float)this.CurrentHP / this.TotalHP);
        }

        /// <summary>
        /// Called when the trigger enters
        /// </summary>
        /// <param name="collision">The collision</param>
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            var proj = collision.gameObject.GetComponent<LuxWProjectile>();
            if (proj != null && this.IsFriendly)
            {
                var newEffect = Instantiate(proj.ShieldEffectPrefab.gameObject).GetComponent<EffectVisuals>();
                newEffect.TargetCharacter = this;
                this.ApplyEffect(EffectEnum.LuxShiled, Config.Lux.ShieldDuration);
                if (this._luxShieldEffect != null)
                {
                    Destroy(this._luxShieldEffect);
                }

                this._luxShieldEffect = newEffect.gameObject;
            }
        }
    }
}