using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class Library
    {
        private string _name;
        private List<Book> _books;
        private List<Member> _members;
        private List<Loan> _loans;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Library name cannot be empty");
                _name = value;
            }
        }

        // Constructor
        public Library(string name)
        {
            Name = name;
            _books = new List<Book>();
            _members = new List<Member>();
            _loans = new List<Loan>();
        }

        // ==================== BOOK MANAGEMENT ====================

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            // Provera da li knjiga već postoji
            if (_books.Any(b => b.ISBN == book.ISBN))
                throw new InvalidOperationException($"Book with ISBN {book.ISBN} already exists");

            _books.Add(book);
            Console.WriteLine($"✓ Book added: {book.Title}");
        }

        public void RemoveBook(string isbn)
        {
            Book book = _books.FirstOrDefault(b => b.ISBN == isbn);

            if (book == null)
                throw new KeyNotFoundException($"Book with ISBN {isbn} not found");

            if (!book.IsAvailable)
                throw new InvalidOperationException("Cannot remove a book that is currently on loan");

            _books.Remove(book);
            Console.WriteLine($"✓ Book removed: {book.Title}");
        }

        public List<Book> GetAllBooks()
        {
            return _books.ToList(); // Vraća kopiju liste
        }

        public List<Book> GetAvailableBooks()
        {
            // LINQ upit
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            // LINQ - case insensitive pretraga
            return _books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public List<Book> SearchBooksByAuthor(string author)
        {
            return _books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();
        }

        public Book GetBookByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }

        // ==================== MEMBER MANAGEMENT ====================

        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (_members.Any(m => m.MemberId == member.MemberId))
                throw new InvalidOperationException($"Member with ID {member.MemberId} already exists");

            _members.Add(member);
            Console.WriteLine($"✓ Member added: {member.Name}");
        }

        public void RemoveMember(string memberId)
        {
            Member member = _members.FirstOrDefault(m => m.MemberId == memberId);

            if (member == null)
                throw new KeyNotFoundException($"Member with ID {memberId} not found");

            // Provera da li član ima aktivne pozajmice
            if (_loans.Any(l => l.Member.MemberId == memberId && !l.IsReturned))
                throw new InvalidOperationException("Cannot remove member with active loans");

            _members.Remove(member);
            Console.WriteLine($"✓ Member removed: {member.Name}");
        }

        public List<Member> GetAllMembers()
        {
            return _members.ToList();
        }

        public Member GetMemberById(string memberId)
        {
            return _members.FirstOrDefault(m => m.MemberId == memberId);
        }

        // ==================== LOAN MANAGEMENT ====================

        public Loan CheckoutBook(string isbn, string memberId, int loanDurationDays = 14)
        {
            // Pronalaženje knjige i člana
            Book book = GetBookByISBN(isbn);
            Member member = GetMemberById(memberId);

            // Validacija
            if (book == null)
                throw new KeyNotFoundException($"Book with ISBN {isbn} not found");

            if (member == null)
                throw new KeyNotFoundException($"Member with ID {memberId} not found");

            if (!book.IsAvailable)
                throw new InvalidOperationException($"Book '{book.Title}' is currently unavailable");

            // Provera da li član ima dovoljno slobodnih slotova
            int currentLoans = _loans.Count(l => l.Member.MemberId == memberId && !l.IsReturned);
            if (currentLoans >= member.GetMaxBooksAllowed())
                throw new InvalidOperationException($"Member has reached maximum loan limit ({member.GetMaxBooksAllowed()} books)");

            // Kreiranje pozajmice
            Loan loan = new Loan(book, member, loanDurationDays);
            book.IsAvailable = false;
            _loans.Add(loan);

            Console.WriteLine($"✓ Book checked out: {book.Title} -> {member.Name}");
            return loan;
        }

        public void ReturnBook(string loanId)
        {
            Loan loan = _loans.FirstOrDefault(l => l.LoanId == loanId);

            if (loan == null)
                throw new KeyNotFoundException($"Loan with ID {loanId} not found");

            if (loan.IsReturned)
                throw new InvalidOperationException("This book has already been returned");

            loan.ReturnBook();

            decimal lateFee = loan.CalculateLateFee();
            if (lateFee > 0)
                Console.WriteLine($"⚠ Late fee: {lateFee:C}");

            Console.WriteLine($"✓ Book returned: {loan.Book.Title}");
        }

        public List<Loan> GetAllLoans()
        {
            return _loans.ToList();
        }

        public List<Loan> GetActiveLoans()
        {
            return _loans.Where(l => !l.IsReturned).ToList();
        }

        public List<Loan> GetOverdueLoans()
        {
            return _loans.Where(l => !l.IsReturned && l.IsOverdue()).ToList();
        }

        public List<Loan> GetLoansByMember(string memberId)
        {
            return _loans.Where(l => l.Member.MemberId == memberId).ToList();
        }

        // ==================== STATISTICS ====================

        public void PrintStatistics()
        {
            Console.WriteLine($"\n{'=',-50}");
            Console.WriteLine($"Library: {Name}");
            Console.WriteLine($"{'=',-50}");
            Console.WriteLine($"Total Books: {_books.Count}");
            Console.WriteLine($"Available Books: {_books.Count(b => b.IsAvailable)}");
            Console.WriteLine($"Books on Loan: {_books.Count(b => !b.IsAvailable)}");
            Console.WriteLine($"Total Members: {_members.Count}");
            Console.WriteLine($"Active Loans: {_loans.Count(l => !l.IsReturned)}");
            Console.WriteLine($"Overdue Loans: {_loans.Count(l => l.IsOverdue())}");
            Console.WriteLine($"{'=',-50}\n");
        }

        public void PrintMostPopularBooks(int topN = 5)
        {
            var popularBooks = _loans
                .GroupBy(l => l.Book.ISBN)
                .Select(g => new
                {
                    Book = g.First().Book,
                    TimesLoaned = g.Count()
                })
                .OrderByDescending(x => x.TimesLoaned)
                .Take(topN);

            Console.WriteLine($"\n=== Top {topN} Most Popular Books ===");
            foreach (var item in popularBooks)
            {
                Console.WriteLine($"{item.Book.Title} - Loaned {item.TimesLoaned} times");
            }
        }
    }
}