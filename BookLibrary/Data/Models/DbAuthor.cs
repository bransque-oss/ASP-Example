namespace Data.Models
{
    public class DbAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DbBook> Books { get; set; }
    }
}
