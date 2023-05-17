namespace Telephony.Models
{
    using Interfaces;
    public class Smartphone : ISmartphone
    {
        public string Browse(string url)
        {
            if (InvalidUrl(url))
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!ValidatePhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Calling... {phoneNumber}";
        }

        private bool InvalidUrl(string url)
            => url.Any(x => char.IsDigit(x));

        private bool ValidatePhoneNumber(string phoneNumber)
            => phoneNumber.All(x => char.IsDigit(x));
    }
}
