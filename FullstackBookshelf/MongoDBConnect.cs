using MongoDB.Driver;
using FullstackBookshelf.Model;

namespace FullstackBookshelf
{
    public class MongoDBConnect
    {
        private readonly IMongoCollection<Book> _collection;

        public MongoDBConnect(string databaseName, string collectionName)
        {
            string connectionString = GetConnectionStringFromEnvFile();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string not found");
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Book>(collectionName);
        }

        private string GetConnectionStringFromEnvFile()
        {
            string envFilePath = "../variables.env";
            if (!File.Exists(envFilePath))
            {
                throw new FileNotFoundException("File containing environment variables not found.");
            }

            string[] lines = File.ReadAllLines(envFilePath);
            foreach (string line in lines)
            {
                if (line.StartsWith("DATABASE_URL="))
                {
                    return line.Substring("DATABASE_URL=".Length);
                }
            }

            throw new InvalidOperationException("Database URL not found.");
        }

        public async Task InsertBookAsync(Book book)
        {
            try
            {
                await _collection.InsertOneAsync(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't insert book: " + ex.Message);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            try
            {
                var filter = Builders<Book>.Filter.Eq(book => book.Id, id);
                await _collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't delete book: " + ex.Message);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                var filter = Builders<Book>.Filter.Empty;
                var books = await _collection.Find(filter).ToListAsync();
                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't get all books: " + ex.Message);
                return new List<Book>();
            }
        }
    }
}