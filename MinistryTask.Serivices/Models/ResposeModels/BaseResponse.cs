namespace MinistryTask.Serivices.Models.ResposeModels
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.Now;
    }
}
