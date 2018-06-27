using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptymalizacjaCięcia
{
    public abstract class Element
    {
        private String profile;
        private String type;
        private int initialLength;
        private int length;

        public Element() { }

        public Element(int len)
        {
            length = len;
            initialLength = len;
        }

        public Element(Element e)
        {
            profile = e.GetProfile();
            type = e.GetElementType();
            initialLength = e.GetInitialLength();
            length = e.GetLength();
        }

        public Element(string profile, string type, int len)
        {
            this.profile = profile;
            this.type = type;
            this.initialLength = len;
            this.length = len;
        }
        
        public String GetProfile()
        {
            return profile;
        }

        public String GetElementType()
        {
            return type;
        }

        public int GetInitialLength()
        {
            return initialLength;
        }

        public int GetLength()
        {
            return length;
        }

        public void SetProfile(String profile)
        {
            this.profile = profile;
        }

        public void SetElementType(String type)
        {
            this.type = type;
        }

        public void SetInitialLength(int length)
        {
            this.initialLength = length;
        }

        public void SetLength(int length)
        {
            this.length = length;
        }

        override public string ToString()
        {
            string ret;

            ret = "Profil: " +  profile + "  Gatunek: " + type + "  Długość: " + initialLength;
            return ret;
        }
    }
}
