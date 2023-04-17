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
            Address = address;
            Header = header;
            Body = body;
            Footer = footer;
            IsPhishing = isPhishing;
        }
    }
}