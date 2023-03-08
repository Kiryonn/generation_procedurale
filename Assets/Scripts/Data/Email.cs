namespace Data
{
    public class Email
    {
        public string address;
        public string header;
        public string body;
        public string footer;

        public Email(string address, string header, string body, string footer)
        {
            this.address = address;
            this.header = header;
            this.body = body;
            this.footer = footer;
        }
    }
}