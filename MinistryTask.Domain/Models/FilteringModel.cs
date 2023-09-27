namespace MinistryTask.Domain.Models
{
    public class FilteringModel
    {
        public string NameFilter { get; set; }
        public string LastNameFilter { get; set; }
        public string GenderFilter { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string PrivateNumber { get; set; }
    }
}
