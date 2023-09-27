namespace MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel
{
    public class FilteringAuthorModel
    {
        public string NameFilter { get; set; }
        public string LastNameFilter { get; set; }
        public string GenderFilter { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string PrivateNumber { get; set; }
    }
}
