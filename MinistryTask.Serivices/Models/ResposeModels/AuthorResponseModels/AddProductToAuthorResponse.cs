namespace MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels
{
    public class AddProductToAuthorResponse : BaseResponse
    {
        public int AuthorId { get; set; }
        public int ProductId { get; set; }

    }
}
