namespace Data {
    public class Email {
        public readonly string Address;
        public readonly string Header;
        public readonly string Body;
        public readonly string Footer;
        public readonly Rules Errors;

        public Email(string address, string header, string body, string footer, Rules errors) {
            Address = address;
            Header = header;
            Body = body;
            Footer = footer;
            Errors = errors;
        }
    }
}