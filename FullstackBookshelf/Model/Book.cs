namespace FullstackBookshelf.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Publisher { get; set; }
        public string DateAdded { get; set; }

        public Book(int id, string title, string author, string genre, string year, string publisher, string dateAdded)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Publisher = publisher;
            DateAdded = dateAdded;
        }
    }
}