using System;

namespace Data
{
    [Serializable]
    public class EmailBlock
    {
        public Information addresses;
        public Information headers;
        public Information bodies;
        public Information footers;
    }
}