using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLACommonLib.Model;

namespace HLACommonLib.Comparer
{
    public class EpcDetailComparer : IEqualityComparer<EpcDetail>
    {
        public bool Equals(EpcDetail x, EpcDetail y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.EPC_SER == y.EPC_SER && x.HU == y.HU;
        }

        public int GetHashCode(EpcDetail obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashHU = obj.HU == null ? 0 : obj.HU.GetHashCode();

            //Get hash code for the Code field.
            int hashEPC = obj.EPC_SER.GetHashCode();

            //Calculate the hash code for the product.
            return hashHU ^ hashEPC;
        }
    }
}
