//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LevelNode.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.LevelSelect
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Shared;

    /// <summary>
    /// Describes node in the levels
    /// </summary>
    public class LevelNode :  MonoBehaviour
    {
        /// <summary>
        /// Index of the level
        /// </summary>
        public int LevelIndex;

        /// <summary>
        /// Called when the level  node is selected
        /// </summary>
        public void OnSelect()
        {
            FadeInOut.CurrentInstance.FadeIn();
            SceneManager.LoadScene(this.LevelIndex);
        }
    }
}
