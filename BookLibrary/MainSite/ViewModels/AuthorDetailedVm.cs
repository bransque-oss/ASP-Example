namespace MainSite.ViewModels
{
    public record AuthorDetailedVm(int Id, string Name, string Description, IEnumerable<BookVm> Books);
}
