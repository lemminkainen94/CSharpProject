using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptymalizacjaCięcia
{
    public class StoredElement : Element, IComparable<StoredElement>
    {
        private string delivery;
        private int storeId;

        public StoredElement() : base() { }

        public StoredElement(int len, int num) : base(len)
        {
            storeId = num;
        }

        public StoredElement(StoredElement e) : base(e)
        {
            delivery = e.GetDelivery();
            storeId = e.GetStoreId();
        }

        public StoredElement(string profile, string type, int len, string delivery, int id) :
            base(profile, type, len)
        {
            this.delivery = delivery;
            storeId = id;
        }

        public int CompareTo(StoredElement e)
        {
            if (this.storeId < e.GetStoreId())
                return -1;
            else if (this.storeId == e.GetStoreId())
                return 0;
            else
                return 1;
        }

        public string GetDelivery()
        {
            return delivery;
        }

        public int GetStoreId()
        {
            return storeId;
        }

        public void SetDelivery(string delivery)
        {
            this.delivery = delivery;
        }

        public void SetStoreId(int storeId)
        {
            this.storeId = storeId;
        }

        override public string ToString()
        {
            string ret = base.ToString();

            ret = ret + "  Nr Dostawy: " + delivery;
            return ret;
        }
    }
}
