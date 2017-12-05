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
        #region Unity Editor links
        /// <summary>
        /// The one and only Mundo controller
        /// </summary>
        public MundoController Mundo;

        /// <summary>
        /// Prefab for the Q projectile
        /// </summary>
        public Projectile QProjectilePrefab;

        /// <summary>
        /// The effect left on the ground when Lee Sin Qs
        /// </summary>
        public GameObject QGroundEffect;

        /// <summary>
        /// The W effect visuals
        /// </summary>
        public EffectVisuals WEffect;

        /// <summary>
        /// THe effect left on the ground when Lee Sin Ws
        /// </summary>
        public GameObject WGroundEffect;

        /// <summary>
        /// Visual effect for LeeSin W's second cast
        /// </summary>
        public EffectVisuals WSecondaryEffect;

        /// <summary>
        /// Effect when LeeSin casts E
        /// </summary>
        public GameObject EGroundEffect;

        /// <summary>
        /// The effect visuals for E
        /// </summary>
        public EffectVisuals EEffect;

        /// <summary>
        /// The effect visuals for the second cast of E
        /// </summary>
        public EffectVisuals ESecondaryEffect;

        /// <summary>
        /// Effect when LeeSin casts R
        /// </summary>
        public GameObject REffect;
        #endregion

        public DashTarget ClosestDashTarget
        {
            get
            {
                var targets = DashTarget.Targets;
                float minSearch = this._isFacingRight ? Config.LeeSin.TargetMinRange : -Config.LeeSin.DashMaxRange;
                float maxSearch = this._isFacingRight ? Config.LeeSin.DashMaxRange : -Config.LeeSin.TargetMinRange;

                DashTarget winner = null;
                float? closest = null;
                foreach (var target in targets)
                {
                    var xDiff = target.transform.position.x - this.transform.position.x;

                    // Check if target is in range
                    if (minSearch < xDiff && xDiff < maxSearch)
                    {
                        // Compared to winner
                        if (closest == null || Math.Abs(xDiff) < closest)
                        {
                            winner = target;
                            closest = Math.Abs(xDiff);
                        }
                    }
                }

                return winner;
            }
        }

        /// <summary>
        /// If LeeSin can R right now
        /// </summary>
        public bool InRRange
        {
            get
            {
                var xDiff = this.Mundo.transform.position.x - this.transform.position.x;
                return (Math.Abs(xDiff) < Config.LeeSin.RRange && (xDiff > 0 == this._isFacingRight));
            }
        }

        /// <summary>
        /// The target
        /// </summary>
        private EnemyController _Qtarget;

        /// <summary>
        /// If LeeSin si in the middle of resonating strike
        /// </summary>
        private bool _isResonating;

        /// <summary>
        /// The dash target
        /// </summary>
        private GameObject _WTarget;

        /// <summary>
        /// If LeeSin is in the middle of a dash
        /// </summary>
        private bool _isDashing;

        /// <summary>
        /// If Lee Sin is facing right
        /// </summary>
        private bool _isFacingRight;

        /// <summary>
        /// The control schema
        /// </summary>
        private InputNames _controls;

        /// <summary>
        /// A list of enemies hit by LeeSin's E
        /// </summary>
        private List<EnemyController> _enemiesHitByE;

        /// <summary>
        /// When the Q landed
        /// </summary>
        public void OnQLanded(EnemyController enemy)
        {
            enemy.TakeDamage(Config.LeeSin.QDamage);
            this._Qtarget = enemy;
        }

        /// <summary>
        /// Try to use Q
        /// </summary>
        private void OnPressQ()
        {
            //  If leesin is in the middle of resonating strike do nothing
            if (this._isResonating)
            {
                return;
            }

            // If there's a Q target, 
            var canFireQ = !this.HasEffect(EffectEnum.QCoolDown);
            if (this._Qtarget != null)
            {
                this._Qtarget.GetComponent<EnemyController>().RemoveEffec(EffectEnum.LeeQLanded);
                if (!canFireQ)
                {
                    var newQGround = Instantiate(this.QGroundEffect);
                    newQGround.transform.position = this.transform.position;
                    this._animator.SetBool("Resonating", true);
                    this._isResonating = true;
                }
                else
                {
                    // Missed window
                    this._Qtarget = null;
                    canFireQ = true;
                }
            }
            
            if(canFireQ)
            {
                var newQ = Instantiate(this.QProjectilePrefab.gameObject).GetComponent<Projectile>();
                newQ.transform.position = this.transform.position;
                newQ.Velocity = this._isFacingRight ? 15 : -15;
                newQ.Duration = 0.5f;
                this._animator.SetBool("HitQ", true);
                this.ApplyEffect(EffectEnum.Snare, 0.2f);
                this.ApplyEffect(EffectEnum.QCoolDown, 3.0f);
            }
        }

        /// <summary>
        /// When the user presses W to cast dash
        /// </summary>
        private void OnPressW()
        {
            float wCoolDown;
            if (!this.Effects.TryGetValue(EffectEnum.WCoolDown, out wCoolDown))
            {
                var winner = this.ClosestDashTarget;

                if (winner != null)
                {
                    var newWGround = Instantiate(this.WGroundEffect);
                    newWGround.transform.position = this.transform.position;
                    this._animator.SetBool("Dashing", true);
                    this._WTarget = winner.gameObject;
                    this._isDashing = true;
                    this.ApplyEffect(EffectEnum.WCoolDown, 3.0f);
                }
            }
            else if (!this.HasEffect(EffectEnum.WSecondaryCooldown))
            {
                this.ApplyEffect(EffectEnum.WSecondaryCooldown, wCoolDown);
                this.ApplyEffect(EffectEnum.LeeWSecondary, 2.0f);
            }
        }

        /// <summary>
        /// Called when the user presses E
        /// </summary>
        private void OnPressE()
        {
            float eCooldown;
            if (!this.Effects.TryGetValue(EffectEnum.ECoolDown, out eCooldown))
            {
                this._enemiesHitByE = new List<EnemyController>();
                foreach (var enemy in EnemyController.Enemies)
                {
                    var xDiff = enemy.transform.position.x - this.transform.position.x;
                    if (Math.Abs(xDiff) < Config.LeeSin.ERange)
                    {
                        this._enemiesHitByE.Add(enemy);
                        enemy.TakeDamage(Config.LeeSin.EDamage);
                        enemy.ApplyEffect(EffectEnum.LeeELanded, 2.0f);
                        var newEEffect = Instantiate(this.EEffect);
                        newEEffect.GetComponent<EffectVisuals>().TargetCharacter = enemy;
                    }
                }

                this._animator.SetBool("HitE", true);
                this.ApplyEffect(EffectEnum.Snare, 0.5f);
                this.ApplyEffect(EffectEnum.ECoolDown, 3.0f);
            }
            else if (!this.HasEffect(EffectEnum.ESecondaryCooldown))
            {
                foreach (var enemy in this._enemiesHitByE)
                {
                    enemy.ApplyEffect(EffectEnum.Slow, 2.0f);
                }
                this.ApplyEffect(EffectEnum.ESecondaryCooldown, eCooldown);
            }
        }

        /// <summary>
        /// Called when the user presses R
        /// </summary>
        private void OnPressR()
        {
            if (this.InRRange && !this.HasEffect(EffectEnum.RCoolDown))
            {
                this._animator.SetBool("HitR", true);
                this.Mundo.TakeDamage(Config.LeeSin.RDamage);
                this.Mundo.ApplyEffect(this._isFacingRight ? EffectEnum.Knockforward : EffectEnum.Knockback, 0.5f);
                this.ApplyEffect(EffectEnum.RCoolDown, 10.0f);
            }
        }

        /// <summary>
        /// Called when the user auto attacks
        /// </summary>
        private void OnAutoAttack()
        {
        }

        /// <summary>
        /// Called when the user places a ward
        /// </summary>
        private void OnPlaceWard()
        {
            if (!this.HasEffect(EffectEnum.WardCoolDown))
            {
                this.PlaceWard(this._isFacingRight);
                this.ApplyEffect(EffectEnum.WardCoolDown, Config.WardCooldown);
            }
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            this._controls = BurneyController.ControlSchema;

            this._isFacingRight = true;

            base.Start();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected override void Update()
        {
            this._animator.SetBool("HitQ", false);
            this._animator.SetBool("HitE", false);
            this._animator.SetBool("HitR", false);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                this.OnPressQ();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.OnPressW();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.OnPressE();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                this.OnPressR();
            }
            if (!this._isResonating && Input.GetKey(KeyCode.Z))
            {
                this.OnPlaceWard();
            }

            // Check if resonating strike is happening
            if (this._Qtarget != null && this._isResonating)
            {
                var movementThisFrame = Config.LeeSin.ResonatingStrikeSpeed * Time.deltaTime;
                var xDiff = this._Qtarget.transform.position.x - this.transform.position.x;
                this._isFacingRight = xDiff > 0;
                this._sprite.flipX = !this._isFacingRight;
                if (Math.Abs(xDiff) < Config.LeeSin.MovementEndDistance)
                {
                    this._Qtarget.TakeDamage(Config.LeeSin.QSecondaryDamage);
                    this._Qtarget = null;
                    this._isResonating = false;
                    this._animator.SetBool("Resonating", false);
                    this.ApplyEffect(EffectEnum.QCoolDown, 3.0f);
                }
                else
                {
                    movementThisFrame *= Math.Sign(xDiff);
                    this.transform.position += new Vector3(movementThisFrame, 0);
                }
            }
            // Check if W dash is happening
            else if (this._WTarget != null && this._isDashing)
            {
                var movementThisFrame = Config.LeeSin.DashSpeed * Time.deltaTime;
                var xDiff = this._WTarget.transform.position.x - this.transform.position.x;
                this._isFacingRight = xDiff > 0;
                this._sprite.flipX = !this._isFacingRight;
                if (Math.Abs(xDiff) < Config.LeeSin.MovementEndDistance)
                {
                    // Reached destination of dash
                    var selfEffect = Instantiate(this.WEffect).GetComponent<EffectVisuals>();
                    selfEffect.TargetCharacter = this;
                    var otherEffect = Instantiate(this.WEffect);
                    otherEffect.transform.position = this._WTarget.transform.position;

                    this._WTarget = null;
                    this._isDashing = false;
                    this._animator.SetBool("Dashing", false);
                    this.ApplyEffect(EffectEnum.LeeShield, 3.0f);
                    this.ApplyEffect(EffectEnum.WCoolDown, 3.0f);
                }
                else
                {
                    movementThisFrame *= Math.Sign(xDiff);
                    this.transform.position += new Vector3(movementThisFrame, 0);
                }
            }
            else
            {
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
            }

            base.Update();
        }
    }
}
