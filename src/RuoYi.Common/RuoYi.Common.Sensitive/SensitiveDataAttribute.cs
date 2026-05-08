namespace RuoYi.Common.Sensitive
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SensitiveDataAttribute : Attribute
    {
        public SensitiveType Type { get; set; } = SensitiveType.Default;

        public SensitiveDataAttribute() { }

        public SensitiveDataAttribute(SensitiveType type)
        {
            Type = type;
        }

        public string Mask(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Type switch
            {
                SensitiveType.ChineseName => MaskChineseName(value),
                SensitiveType.PhoneNumber => MaskPhoneNumber(value),
                SensitiveType.Email => MaskEmail(value),
                SensitiveType.IdCard => MaskIdCard(value),
                SensitiveType.BankCard => MaskBankCard(value),
                SensitiveType.Address => MaskAddress(value),
                SensitiveType.Password => "******",
                _ => value
            };
        }

        private static string MaskChineseName(string name)
        {
            if (name.Length <= 1) return name;
            return name[0] + "**";
        }

        private static string MaskPhoneNumber(string phone)
        {
            if (phone.Length < 11) return phone;
            return phone[..3] + "****" + phone[^4..];
        }

        private static string MaskEmail(string email)
        {
            var atIndex = email.IndexOf('@');
            if (atIndex <= 1) return email;
            return email[0] + "***" + email[atIndex..];
        }

        private static string MaskIdCard(string idCard)
        {
            if (idCard.Length < 15) return idCard;
            return idCard[..6] + "********" + idCard[^4..];
        }

        private static string MaskBankCard(string card)
        {
            if (card.Length < 10) return card;
            return card[..4] + " **** **** " + card[^4..];
        }

        private static string MaskAddress(string address)
        {
            if (address.Length <= 6) return address;
            return address[..6] + "****";
        }
    }

    public enum SensitiveType
    {
        Default,
        ChineseName,
        PhoneNumber,
        Email,
        IdCard,
        BankCard,
        Address,
        Password
    }
}