namespace MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel
{
    public class FilterAuthorsModel
    {
        public string NameFilter { get; set; }
        public string LastNamefilter { get; set; }
        public string GenderFilter { get; set; }
        public int Page { get; set; }
        public int Pagesize { get; set; }
    }
}
