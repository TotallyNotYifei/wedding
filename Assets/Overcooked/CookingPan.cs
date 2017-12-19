using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Overcooked
{
    public class CookingPan : CookingContainer
    {
        public override HoldableTypes HoldableType
        {
            get
            {
                return HoldableTypes.Pan;
            }
        }
    }
}
