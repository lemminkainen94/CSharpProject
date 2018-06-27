using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptymalizacjaCięcia
{
    public class CommissionElement : Element, IComparable<CommissionElement>
    {
        private int listing;

        public CommissionElement() : base() { }

        public CommissionElement(int len) : base(len) { }

        public CommissionElement(CommissionElement e) : base(e)
        {
            listing = e.GetListing();
        }

        public CommissionElement(string profile, string type, int len, int listing) :
            base(profile, type, len)
        {
            this.listing = listing;
        }

        public int CompareTo(CommissionElement e)
        {
            if (this.GetLength() < e.GetLength())
                return -1;
            else if (this.GetLength() == e.GetLength())
                return 0;
            else
                return 1;
        }

        public int GetListing()
        {
            return listing;
        }

        public void SetListing(int listing)
        {
            this.listing = listing;
        }

        public override string ToString()
        {
            string ret = base.ToString();

            ret = "Pozycja: " + listing + "  " + ret;
            return ret;
        }
    }
}
