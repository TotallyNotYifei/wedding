//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TrashCan.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overcooked
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TrashCan : OvercookedMapObject
    {
        public override bool IsEmpty
        {
            get
            {
                return true;
            }
        }

        public override IHoldable Peek()
        {
            return null;
        }

        public override IHoldable RetrieveContent()
        {
            return null;
        }

        public override bool TryAdd(IHoldable newItem)
        {
            newItem.Dump();
            return (!(newItem is IContainer));
        }
    }
}
