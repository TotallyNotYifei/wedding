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
        /// How far away the wards are placed
        /// </summary>
        public const float WardPlacementRange = 2.5f;

        /// <summary>
        /// How long the wards last
        /// </summary>
        public const float WardCooldown = 6.0f;
    }
}
