namespace LibraryManagementSystem.Models
{
    public class StudentMember : Member
    {
        private string _studentId;
        private string _university;

        public string StudentId
        {
            get { return _studentId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Student ID cannot be empty");
                _studentId = value;
            }
        }

        public string University
        {
            get { return _university; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("University cannot be empty");
                _university = value;
            }
        }

        // Constructor - poziva base constructor
        public StudentMember(string memberId, string name, string email, string studentId, string university)
            : base(memberId, name, email)
        {
            StudentId = studentId;
            University = university;
        }

        // Override abstract metode - studenti mogu da pozajme više knjiga
        public override int GetMaxBooksAllowed()
        {
            return 5;
        }

        // Override virtual metode
        public override string GetMemberType()
        {
            return "Student Member";
        }

        public override string ToString()
        {
            return base.ToString() + $" | {University}";
        }
    }
}