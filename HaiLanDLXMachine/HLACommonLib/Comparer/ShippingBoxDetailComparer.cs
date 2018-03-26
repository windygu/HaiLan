using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLACommonLib.Model;

namespace HLACommonLib.Comparer
{
    public class ShippingBoxDetailComparer : IEqualityComparer<ShippingBoxDetail>
    {

        public bool Equals(ShippingBoxDetail x, ShippingBoxDetail y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.PICK_TASK == y.PICK_TASK && x.PICK_TASK_ITEM == y.PICK_TASK_ITEM && x.HU == y.HU;
        }

        public int GetHashCode(ShippingBoxDetail obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashPICK_TASK = obj.PICK_TASK == null ? 0 : obj.PICK_TASK.GetHashCode();

            //Get hash code for the Code field.
            int hashPICK_TASK_ITEM = obj.PICK_TASK_ITEM.GetHashCode();

            int hashHU = obj.HU.GetHashCode();

            //Calculate the hash code for the product.
            return hashHU ^ hashPICK_TASK ^ hashPICK_TASK_ITEM;
        }
    }
}
