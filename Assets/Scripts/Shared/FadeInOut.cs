//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FadeInOut.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Shared
{
    /// <summary>
    /// A black over canvas to control fade in/out
    /// </summary>
    public class FadeInOut
    {
        /// <summary>
        /// Creates a new instance of the <see cref="FadeInOut"/> class
        /// </summary>
        public FadeInOut()
        {
            FadeInOut.CurrentInstance = this;
        }

        /// <summary>
        /// Gets the current instance of the <see cref="FadeInOut"/> class
        /// </summary>
        public static FadeInOut CurrentInstance { get; private set; }


        /// Causes the black to appear
        /// </summary>
        public void FadeIn()
        {

        }
    }
}
