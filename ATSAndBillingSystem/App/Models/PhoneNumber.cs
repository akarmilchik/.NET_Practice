namespace ATEAndBillingSystem.App.Models
{
    public struct PhoneNumber
    {
        private string _phoneNumber;

        public string Value
        {
            get { return _phoneNumber; }
            private set { _phoneNumber = value; }
        }

        public PhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber)
            {
                return this._phoneNumber == ((PhoneNumber)obj)._phoneNumber;
            }
            else
            {
                return false;
            }
        }

    }
}
