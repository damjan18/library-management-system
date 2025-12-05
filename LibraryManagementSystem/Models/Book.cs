namespace LibraryManagementSystem.Models
{
    public class Book
    {
        // Private fields (enkapsulacija)
        private string _isbn;
        private string _title;
        private string _author;
        private int _publicationYear;
        private bool _isAvailable;

        // Properties sa validacijom
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ISBN cannot be empty");
                _isbn = value;
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty");
                _title = value;
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be empty");
                _author = value;
            }
        }

        public int PublicationYear
        {
            get { return _publicationYear; }
            set
            {
                if (value < 1000 || value > DateTime.Now.Year)
                    throw new ArgumentException("Invalid publication year");
                _publicationYear = value;
            }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }

        // Constructor
        public Book(string isbn, string title, string author, int publicationYear)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            IsAvailable = true; // Nova knjiga je dostupna
        }

        // Metoda za prikaz informacija o knjizi
        public override string ToString()
        {
            string availability = IsAvailable ? "Available" : "Checked out";
            return $"[{ISBN}] {Title} by {Author} ({PublicationYear}) - {availability}";
        }
    }
}