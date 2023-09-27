using MinistryTask.Domain.Models;

namespace MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels
{
    public class GetAuthorsWithFiltersResponse
    {
        public IEnumerable<Author> Authors { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
