using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLACommonLib.Model;

namespace HLACommonLib.Comparer
{
    public class InventoryOutLogDetailComparer : IEqualityComparer<InventoryOutLogDetailInfo>
    {
        public bool Equals(InventoryOutLogDetailInfo x, InventoryOutLogDetailInfo y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.PICK_TASK == y.PICK_TASK;
        }

        public int GetHashCode(InventoryOutLogDetailInfo obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashPICK_TASK = obj.PICK_TASK == null ? 0 : obj.PICK_TASK.GetHashCode();

            //Calculate the hash code for the product.
            return hashPICK_TASK;
        }
    }
}
