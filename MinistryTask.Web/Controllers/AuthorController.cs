using Microsoft.AspNetCore.Mvc;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels;

namespace MinistryTask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("addAuthor")]

        public async Task<ActionResult<AddAuthorResponse>> AddNewAuthor(AuthorDto entity)
        {
            var result = await _authorService.AddAuthor(entity);
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return result;
            }
            return BadRequest(result);
        }

        [HttpGet("getFullAuthorInfoById")]
        public async Task<ActionResult<GetAuthorInfoResponse>> GetAuthorInfoById(int id)
        {
            var result = await _authorService.GetAuthorFullInfoById(id);
            //400 is dabruneba statuskodebshi
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("RemoveAuthor")]
        public async Task<ActionResult<DeleteAuthorResponse>> RemoveAuthor(int authorId)
        {
            await _authorService.DeleteAuthor(authorId);
            return Ok(new DeleteAuthorResponse
            {
                ActionDate = DateTime.UtcNow,
                StatusCode = StatusCodes.Status200OK,
                Message = "ავტორი წარმატებით წაიშალა სისტემიდან"
            });
        }
        [HttpDelete("RemoveProductFromAuthor")]
        public async Task<ActionResult<DeleteProductToAuthorResponse>> RemoveProductFromAuthor(int authorId, int productToDelete)
        {
            await _authorService.DeleteProductToAuthor(authorId, productToDelete);
            return Ok(new DeleteProductToAuthorResponse
            {
                ProductId = productToDelete,
                ActionDate = DateTime.UtcNow,
                StatusCode = StatusCodes.Status200OK,
                Message = "პროდუქტი წარმატებით წაიშალა ავტორის სიიდან."
            });
        }

        [HttpPost("AddProductToAuthor")]
        public async Task<ActionResult<AddProductToAuthorResponse>> AddProductToAuthorList(int id, ProductDto entity)
        {
            await _authorService.AddProductToAuthor(id, entity);
            return Ok(new AddProductToAuthorResponse
            {
                ActionDate = DateTime.UtcNow,
                Message = "პროდუქტი წარმატებით დაემატა ავტორის ცხრილში",
                AuthorId = id,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPatch("UpdateAuthor/{AuthorId}")]
        public async Task<ActionResult<UpdateAuthorResponse>> UpdateAuthor(AuthorDto entity, int id)
        {
            return await _authorService.UpdateAuthor(entity, id);
        }

        [HttpGet("GetFilteredAuthors")]
        public async Task<ActionResult<GetAuthorsWithFiltersResponse>> GetFilteredAuthors([FromQuery] string nameFilter, string lastnameFilter, string genderFilter, int page, int pageSize, string privateNumber)
        {
            FilteringAuthorModel model = new FilteringAuthorModel
            {
                GenderFilter = genderFilter,
                LastNameFilter = lastnameFilter,
                NameFilter = nameFilter,
                Page = page,
                PageSize = pageSize,
                PrivateNumber = privateNumber
            };
            return await _authorService.GetAuthorsWithFilters(model);
        }
    }
}
