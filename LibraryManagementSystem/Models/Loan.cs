namespace LibraryManagementSystem.Models
{
    public class Loan
    {
        public string LoanId { get; set; }
        public Book Book { get; set; }
        public Member Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable - može biti null ako knjiga nije vraćena
        public bool IsReturned { get; set; }

        // Constructor
        public Loan(Book book, Member member, int loanDurationDays = 14)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            LoanId = Guid.NewGuid().ToString().Substring(0, 8);
            Book = book;
            Member = member;
            LoanDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(loanDurationDays);
            IsReturned = false;
            ReturnDate = null;
        }

        // Metoda za vraćanje knjige
        public void ReturnBook()
        {
            if (IsReturned)
                throw new InvalidOperationException("Book has already been returned");

            IsReturned = true;
            ReturnDate = DateTime.Now;
            Book.IsAvailable = true;
        }

        // Provera da li je knjiga zakasnila
        public bool IsOverdue()
        {
            if (IsReturned)
                return false;

            return DateTime.Now > DueDate;
        }

        // Računanje kazne za kašnjenje
        public decimal CalculateLateFee(decimal feePerDay = 0.50m)
        {
            if (!IsOverdue())
                return 0;

            int daysLate = (DateTime.Now - DueDate).Days;
            return daysLate * feePerDay;
        }

        public override string ToString()
        {
            string status = IsReturned ? $"Returned on {ReturnDate:dd/MM/yyyy}" :
                            IsOverdue() ? $"OVERDUE! Due: {DueDate:dd/MM/yyyy}" :
                            $"Due: {DueDate:dd/MM/yyyy}";

            return $"[{LoanId}] {Book.Title} -> {Member.Name} | {status}";
        }
    }
}