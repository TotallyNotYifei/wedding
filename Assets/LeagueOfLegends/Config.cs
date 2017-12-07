//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Config.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.LeagueOfLegends
{
    /// <summary>
    /// A collection of configurations
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// All Lee Sin related configurations
        /// </summary>
        public static class LeeSin
        {
            /// <summary>
            /// Speed of the resonating strike
            /// </summary>
            public const float ResonatingStrikeSpeed = 8.0f;

            /// <summary>
            /// How fast LeeSin's W goes
            /// </summary>
            public const float DashSpeed = 12.0f;

            /// <summary>
            /// The distance that will consider resonating strike done
            /// </summary>
            public const float MovementEndDistance =0.4f;

            /// <summary>
            /// Range settings
            /// </summary>
            public const float TargetMinRange = 0.5f;

            /// <summary>
            /// Max dash range
            /// </summary>
            public const float DashMaxRange = 3.5f;

            /// <summary>
            /// Range of the initial E skill
            /// </summary>
            public const float ERange = 4.0f;

            /// <summary>
            /// Ult's max range
            /// </summary>
            public const float RRange =1.0f;

            /// <summary>
            /// Damage for auto attack
            /// </summary>
            public const float AutoDamage = 50;

            /// <summary>
            /// Damage for the first cast of Q
            /// </summary>
            public const float QDamage = 120;

            /// <summary>
            /// Damage for resonating strike
            /// </summary>
            public const float QSecondaryDamage = 140;

            /// <summary>
            /// Damage for the fist slam
            /// </summary>
            public const float EDamage = 200;

            /// <summary>
            /// Damage for ult
            /// </summary>
            public const float RDamage = 420;

            /// <summary>
            /// Damage taken from getting hit by a target knocked back
            /// </summary>
            public const float RKnockupDamage = 300;
        }

        /// <summary>
        /// All Lux related configurations
        /// </summary>
        public static class Lux
        {
            /// <summary>
            /// How far Lux/Leesin will have to be to the projectile so they'll receive the shield
            /// </summary>
            public const float WApplyShieldRange = 0.5f;

            /// <summary>
            /// How long the lux's shield lasts
            /// </summary>
            public const float ShieldDuration = 2.5f;

            /// <summary>
            /// How far the returning W have to be for Lux to catch it
            /// </summary>
            public const float LuxCatchReturningWRange = 0.5f;

            /// <summary>
            /// Range for the laser
            /// </summary>
            public const float RRange = 12;

            /// <summary>
            /// Range for Lux's ult
            /// </summary>
            public const float RPlacementRage = 4.65f;

            /// <summary>
            /// Cooldown for the W skill
            /// </summary>
            public const float QCoolDown = 3.0f;

            /// <summary>
            /// Cooldown for the W skill
            /// </summary>
            public const float WCoolDown = 3.0f;

            /// <summary>
            /// Cooldown for the W skill
            /// </summary>
            public const float ECoolDown = 3.0f;

            /// <summary>
            /// How long the ultimate's cooldown is
            /// </summary>
            public const float RCoolDown = 10.0f;

            /// <summary>
            /// Damage for lux's Q
            /// </summary>
            public const float QDamage = 240;

            /// <summary>
            /// How much E does when detonated
            /// </summary>
            public const float EDamage = 350;

            /// <summary>
            /// Damage for Lux's ult
            /// </summary>
            public const float RDamage = 700;

            /// <summary>
            /// How much damage it'll do to detonate Lux's mark
            /// </summary>
            public const float DetonateMarkDamage = 70;
        }

        /// <summary>
        /// How far away the wards are placed
        /// </summary>
        public const float WardPlacementRange = 2.5f;

        /// <summary>
        /// How long the wards last
        /// </summary>
        public const float WardCooldown = 6.0f;
    }
}
