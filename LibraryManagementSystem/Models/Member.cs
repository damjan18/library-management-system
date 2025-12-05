namespace LibraryManagementSystem.Models
{
    public abstract class Member
    {
        // Private fields
        private string _memberId;
        private string _name;
        private string _email;
        private DateTime _membershipDate;

        // Properties
        public string MemberId
        {
            get { return _memberId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Member ID cannot be empty");
                _memberId = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                _name = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("Invalid email address");
                _email = value;
            }
        }

        public DateTime MembershipDate
        {
            get { return _membershipDate; }
            set { _membershipDate = value; }
        }

        // Constructor
        protected Member(string memberId, string name, string email)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            MembershipDate = DateTime.Now;
        }

        // Abstract method
        public abstract int GetMaxBooksAllowed();

        // Virtual method
        public virtual string GetMemberType()
        {
            return "General Member";
        }

        public override string ToString()
        {
            return $"[{MemberId}] {Name} ({GetMemberType()}) - Max books: {GetMaxBooksAllowed()}";
        }
    }
}