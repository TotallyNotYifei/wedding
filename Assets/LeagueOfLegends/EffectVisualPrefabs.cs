//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EffectVisualPrefabs.cs">
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
    /// Keeps a collection of effect visuals
    /// </summary>
    public class EffectVisualPrefabs : MonoBehaviour
    {
        /// <summary>
        /// All of the effect visual prefabs
        /// </summary>
        public List<KeyValuePair<EffectEnum, EffectVisuals>> Prefabs;

        /// <summary>
        /// A dictionary of effect => visual prefab
        /// </summary>
        private static Dictionary<EffectEnum, EffectVisuals> _prefabHash;

        public static EffectVisuals GetPrefab(EffectEnum effect)
        {
            EffectVisuals result;
            if (EffectVisualPrefabs._prefabHash.TryGetValue(effect, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            EffectVisualPrefabs._prefabHash = new Dictionary<EffectEnum, EffectVisuals>();

            foreach (var pair in this.Prefabs)
            {
                EffectVisualPrefabs._prefabHash[pair.Key] = pair.Value;
            }
        }
    }
}
