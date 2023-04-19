using System;

namespace Data {
    [Serializable]
    public class EmailData {
        public string[] addresses;
        public string[] headers;
        public string[] bodies;
        public string[] footers;
    }
}