namespace Services.Models
{
    public record ResponseAuthor(int Id, string Name, string Description, IEnumerable<ResponseBook>? Books);
}
