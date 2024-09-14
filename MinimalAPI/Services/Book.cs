namespace MinimalAPI.Services
{
    public interface IBookServices
    {
        List<Book> GetBooks();
        Book GetBook(int id);
        void AddBook(Book book);
        void UpdateBook(int id, Book book);
        void DeleteBook(int id);

    }
    public class BookServices : IBookServices
    {
        private readonly List<Book> _books;
        public BookServices()
        {
            _books = new List<Book>
            {
                 new Book
                 {
                     Id = 1,
                     Title = "Integration With API",
                     Author = "Aya Saad",
                 },
                 new Book
                 {
                     Id= 2,
                     Title = "ASP .NET Core",
                     Author = "Taha Mohammed"
                 },
                 new Book
                 {
                     Id = 3,
                     Title = "Minimal API",
                     Author = "Hoor Taha"
                 }
            };
        }
        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void DeleteBook(int id)
        {
            var removedBook = _books.FirstOrDefault(i => i.Id == id);
            if(removedBook != null)
                _books.Remove(removedBook);
        }

        public Book GetBook(int id)
        {
            return _books.FirstOrDefault(i => i.Id == id);
        }

        public List<Book> GetBooks()
        {
            return _books;
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = _books.FirstOrDefault(i => i.Id == id);
            if(existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
            }
        }
    }
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
    }
}
