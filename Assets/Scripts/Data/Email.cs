namespace Data
{
    public class Email
    {
        public readonly string Address;
        public readonly string Header;
        public readonly string Body;
        public readonly string Footer;
        public readonly bool IsPhishing;

        public Email(string address, string header, string body, string footer, bool isPhishing)
        {
            this.Address = address;
            this.Header = header;
            this.Body = body;
            this.Footer = footer;
            this.IsPhishing = isPhishing;
        }
    }
}