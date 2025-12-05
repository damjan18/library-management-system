using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("-----LIBRARY MANAGEMENT SYSTEM-----");
                

                // Creating library
                Library library = new Library("Belgrade City Library");

                // Adding Books
                Console.WriteLine("=== ADDING BOOKS ===");
                library.AddBook(new Book("978-0-545-01022-1", "Harry Potter and the Philosopher's Stone", "J.K. Rowling", 1997));
                library.AddBook(new Book("978-0-061-96436-7", "To Kill a Mockingbird", "Harper Lee", 1960));
                library.AddBook(new Book("978-0-141-43951-8", "1984", "George Orwell", 1949));
                library.AddBook(new Book("978-0-743-27356-5", "The Great Gatsby", "F. Scott Fitzgerald", 1925));
                library.AddBook(new Book("978-0-316-76948-0", "The Catcher in the Rye", "J.D. Salinger", 1951));

                // Adding members
                Console.WriteLine("\n=== ADDING MEMBERS ===");
                library.AddMember(new StudentMember("M001", "Marko Petrovic", "marko@student.rs", "S12345", "University of Belgrade"));
                library.AddMember(new StudentMember("M002", "Jovana Nikolic", "jovana@student.rs", "S12346", "University of Novi Sad"));
                library.AddMember(new RegularMember("M003", "Ana Jovanovic", "ana@email.com", "+381601234567"));
                library.AddMember(new RegularMember("M004", "Nikola Djordjevic", "nikola@email.com", "+381602345678"));

                // Statistics
                library.PrintStatistics();

                // Loaning books
                Console.WriteLine("=== CHECKING OUT BOOKS ===");
                library.CheckoutBook("978-0-545-01022-1", "M001"); // Harry Potter -> Marko
                library.CheckoutBook("978-0-061-96436-7", "M001"); // To Kill a Mockingbird -> Marko
                library.CheckoutBook("978-0-141-43951-8", "M003"); // 1984 -> Ana
                library.CheckoutBook("978-0-743-27356-5", "M002"); // The Great Gatsby -> Jovana

                // Available books
                Console.WriteLine("\n=== AVAILABLE BOOKS ===");
                var availableBooks = library.GetAvailableBooks();
                foreach (var book in availableBooks)
                {
                    Console.WriteLine(book);
                }

                // Active loans
                Console.WriteLine("\n=== ACTIVE LOANS ===");
                var activeLoans = library.GetActiveLoans();
                foreach (var loan in activeLoans)
                {
                    Console.WriteLine(loan);
                }

                // Book search
                Console.WriteLine("\n=== SEARCH: Books containing 'the' ===");
                var searchResults = library.SearchBooksByTitle("the");
                foreach (var book in searchResults)
                {
                    Console.WriteLine(book);
                }

                // Returning book
                Console.WriteLine("\n=== RETURNING BOOKS ===");
                var firstLoan = activeLoans.First();
                library.ReturnBook(firstLoan.LoanId);

                // New statistic
                library.PrintStatistics();

                // Most popular books
                library.PrintMostPopularBooks(3);

                // Test exception handling
                Console.WriteLine("\n=== TESTING ERROR HANDLING ===");
                try
                {
                   
                    library.CheckoutBook("978-0-141-43951-8", "M004");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Expected error: {ex.Message}");
                }

                // Loans per member
                Console.WriteLine("\n=== LOANS BY MEMBER (Marko) ===");
                var markoLoans = library.GetLoansByMember("M001");
                foreach (var loan in markoLoans)
                {
                    Console.WriteLine(loan);
                }

                Console.WriteLine("-----COMPLETE-----");
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}