namespace LibraryManagementSystem.Models
{
    public class RegularMember : Member
    {
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be empty");
                _phoneNumber = value;
            }
        }

        // Constructor
        public RegularMember(string memberId, string name, string email, string phoneNumber)
            : base(memberId, name, email)
        {
            PhoneNumber = phoneNumber;
        }

        // Override abstract method
        public override int GetMaxBooksAllowed()
        {
            return 3;
        }

        // Override virtual method
        public override string GetMemberType()
        {
            return "Regular Member";
        }

        public override string ToString()
        {
            return base.ToString() + $" | Phone: {PhoneNumber}";
        }
    }
}