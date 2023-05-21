namespace Data.Models
{
    public class DbBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }

        public int AuthorId { get; set; }
        public DbAuthor Author { get; set; }
    }
}
