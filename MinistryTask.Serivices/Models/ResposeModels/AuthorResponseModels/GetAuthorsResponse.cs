using MinistryTask.Domain.Models;

namespace MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels
{
    public class GetAuthorsResponse : BaseResponse
    {
        public List<Author> Authors { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PrevioiusPage { get; set; }
        public int NextPage { get; set; }
    }
}
