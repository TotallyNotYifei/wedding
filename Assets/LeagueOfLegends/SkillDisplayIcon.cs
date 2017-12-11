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
    using UnityEngine.UI;

    /// <summary>
    /// Displays the availability and cooldown for skills
    /// </summary>
    public class SkillDisplayIcon : MonoBehaviour
    {
        /// <summary>
        /// Primary cooldown 
        /// </summary>
        public EffectEnum PrimaryEffect;

        /// <summary>
        /// The target character
        /// </summary>
        public Character TargetCharacter;

        /// <summary>
        /// The cover object
        /// </summary>
        public GameObject CoverObject;

        /// <summary>
        /// sprite for the icon
        /// </summary>
        public Image IconSprite;

        /// <summary>
        /// To display percentage
        /// </summary>
        public float TotalCooldown;

        /// <summary>
        /// Secondary icon
        /// </summary>
        public Sprite SecondaryIcon;
        
        /// <summary>
        /// Secondary cooldown
        /// </summary>
        public EffectEnum SecondaryEffect;
        
        /// <summary>
        /// If this icon has secondary effects
        /// </summary>
        private bool _hasSecondaryEffect;

        /// <summary>
        /// The  usual sprite
        /// </summary>
        private Sprite _primarySprite;

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._hasSecondaryEffect = this.SecondaryIcon != null;
            this._primarySprite = this.IconSprite.sprite;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            float primaryCD = this.TargetCharacter.GetEffectDuration(this.PrimaryEffect);
            float finalPercent = primaryCD / this.TotalCooldown;

            this.IconSprite.sprite = this._primarySprite;

            if(primaryCD > 0 && this._hasSecondaryEffect  && !this.TargetCharacter.HasEffect(this.SecondaryEffect))
            {
                this.IconSprite.sprite = this.SecondaryIcon ;
                finalPercent = this.TargetCharacter.GetEffectDuration(this.SecondaryEffect) / this.TotalCooldown;
            }

            Debug.Log(finalPercent);
            this.CoverObject.transform.localScale = new Vector3(finalPercent, 1, 1);
        }
    }
}
