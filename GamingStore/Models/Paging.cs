namespace GamingStore.Models
{
    public abstract class Paging
    {
        public int CurrentPage { get; init; }

        public int ItemsPerPage { get; init; }

        public int TotalItems { get; init; }

        public int PagesCount => (int)Math.Ceiling((double)this.TotalItems / this.ItemsPerPage);

        public bool HasNextPage => this.CurrentPage < this.PagesCount;

        public bool HasPreviousPage => this.CurrentPage > 1;

        public int NextPageNumber => this.CurrentPage + 1;

        public int PreviousPageNumber => this.CurrentPage - 1;
    }
}
