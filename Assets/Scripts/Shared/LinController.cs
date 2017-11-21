//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LinController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Controller for Lin
    /// </summary>
    public abstract class LinController : BaseController
    {
        protected override int _controllerIndex
        {
            get
            {
                return 1;
            }
        }
    }
}
