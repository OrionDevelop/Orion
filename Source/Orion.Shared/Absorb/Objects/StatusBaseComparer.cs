using System;
using System.Collections.Generic;

namespace Orion.Shared.Absorb.Objects
{
    internal class StatusBaseComparer : IComparer<StatusBase>
    {
        public int Compare(StatusBase x, StatusBase y)
        {
            if (x == null || y == null)
                throw new ArgumentException();

            if (x.Id > y.Id)
                return 1;
            return x.Id > y.Id ? -1 : 0;
        }
    }
}