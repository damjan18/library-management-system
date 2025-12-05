# Library Management System

A console-based library management system built in C# demonstrating core Object-Oriented Programming principles and modern C# features.

## Project Purpose

This project showcases practical implementation of:
- **Object-Oriented Programming** (OOP) principles
- **LINQ** queries for data manipulation
- **Exception handling** and input validation
- **Collections** and data structures
- Clean code architecture

## Features

### Book Management
- Add and remove books from the library catalog
- Search books by title or author
- Track book availability status
- View all books or filter available ones

### Member Management
- Support for different member types (Students and Regular members)
- Different borrowing limits based on member type
  - Student Members: 5 books maximum
  - Regular Members: 3 books maximum
- Member registration and removal

### Loan Management
- Check out books to members
- Return books with automatic availability update
- Calculate late fees for overdue books (€0.50 per day)
- Track loan history
- View active and overdue loans

### Statistics & Reports
- Library overview statistics
- Most popular books report
- Member loan history
- Overdue loans tracking

## Architecture

### Class Structure
```
LibraryManagementSystem/
├── Models/
│   ├── Book.cs              # Book entity with validation
│   ├── Member.cs            # Abstract base class for members
│   ├── StudentMember.cs     # Student member implementation
│   ├── RegularMember.cs     # Regular member implementation
│   └── Loan.cs              # Loan tracking with due dates
└── Services/
    └── Library.cs           # Core library management logic
```

### Key OOP Concepts Demonstrated

#### 1. Encapsulation
```
private string _title;
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
```

#### 2. Inheritance
```
public abstract class Member { ... }
public class StudentMember : Member { ... }
public class RegularMember : Member { ... }
```

#### 3. Polymorphism
```
public abstract int GetMaxBooksAllowed();

// StudentMember
public override int GetMaxBooksAllowed() => 5;

// RegularMember
public override int GetMaxBooksAllowed() => 3;
```

#### 4. Abstraction
```
public abstract class Member
{
    public abstract int GetMaxBooksAllowed();
    public virtual string GetMemberType() { ... }
}
```

## Technologies Used

- **Language:** C# 12.0
- **Framework:** .NET 8.0
- **IDE:** Visual Studio 2022 Community
- **Data Structures:** List<T>, LINQ queries
- **Design Patterns:** Repository pattern concepts

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 8.0 SDK or later

### Installation

1. Clone the repository:
```
git clone https://github.com/YOUR_USERNAME/LibraryManagementSystem.git
```

2. Open the solution in Visual Studio:
```
cd LibraryManagementSystem
start LibraryManagementSystem.sln
```

3. Build and run the project:
   - Press `F5` to run with debugging
   - Or press `Ctrl+F5` to run without debugging

## Usage Examples

### Adding Books
```
library.AddBook(new Book("978-0-545-01022-1", "Harry Potter", "J.K. Rowling", 1997));
```

### Adding Members
```
library.AddMember(new StudentMember("M001", "John Doe", "john@student.com", "S12345", "University of Belgrade"));
```

### Checking Out Books
```
library.CheckoutBook("978-0-545-01022-1", "M001", 14); // 14 days loan
```

### Searching Books
```
var results = library.SearchBooksByTitle("Harry");
var authorBooks = library.SearchBooksByAuthor("Rowling");
```

### Getting Statistics
```
library.PrintStatistics();
library.PrintMostPopularBooks(5);
```

## Error Handling

The system includes comprehensive error handling:
- Input validation for all entities
- Duplicate prevention (ISBN, Member ID)
- Business rule validation (loan limits, book availability)
- Meaningful exception messages

## LINQ Examples in Project
```
// Get available books
_books.Where(b => b.IsAvailable).ToList();

// Search with case-insensitive
_books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();

// Get overdue loans
_loans.Where(l => !l.IsReturned && l.IsOverdue()).ToList();

// Most popular books
_loans.GroupBy(l => l.Book.ISBN)
      .Select(g => new { Book = g.First().Book, Count = g.Count() })
      .OrderByDescending(x => x.Count);
```

## Learning Outcomes

By studying this project, you can learn:
- How to structure a C# application with proper separation of concerns
- Implementation of OOP principles in a real-world scenario
- Effective use of LINQ for data querying
- Exception handling and input validation strategies
- Working with DateTime and nullable types
- Collection manipulation and filtering

## Future Enhancements

Potential features to add:
-  Database integration (SQL Server / SQLite)
- User authentication system
- Book reservations
- Email notifications for due dates
- Export reports to PDF/CSV
- Web API / REST endpoints
- Unit tests with xUnit



## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.

# User-specific files
*.rsuser
*.suo
*.user
*.userosscache
*.sln.docstates

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Aa][Rr][Mm]/
[Aa][Rr][Mm]64/
bld/
[Bb]in/
[Oo]bj/
[Ll]og/
[Ll]ogs/

