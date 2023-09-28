using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels;
using System.Security.Claims;

namespace MinistryTask.Web.Controllers
{
    [Authorize]
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
            Serilog.Log.Information("Reached GetFilteredAuthors Endpoint");

            var result = await _authorService.AddAuthor(entity);
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return result;
            }
            Serilog.Log.Information("Author could not been added returned BadRequest");
            return BadRequest(result);
        }

        [HttpGet("getFullAuthorInfoById")]
        public async Task<ActionResult<GetAuthorInfoResponse>> GetAuthorInfoById(int id)
        {
            Serilog.Log.Information("Reached getFullAuthorInfoById Endpoint");
            var result = await _authorService.GetAuthorFullInfoById(id);
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(result);
            }
            Serilog.Log.Information("Author Could Not been found Retuned Bad Request");
            return BadRequest(result);
        }
        [HttpDelete("RemoveAuthor")]
        public async Task<ActionResult<DeleteAuthorResponse>> RemoveAuthor(int authorId)
        {
            Serilog.Log.Information("Reached RemoveAuthor Endpoint");
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
            Serilog.Log.Information("Reached RemoveProductFromAuthor Endpoint");
            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role.Value == "Manager")
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
            Serilog.Log.Information("Returned Unauthorized On RemoveProductFromAuthor Endpoint");
            return Unauthorized(new DeleteProductToAuthorResponse
            {

                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "თქვენ არ გაქვთ წვდომა ამ მოქმედებაზე",
                ActionDate = DateTime.Now,
                ProductId = productToDelete
            });
        }

        [HttpPost("AddProductToAuthor")]
        public async Task<ActionResult<AddProductToAuthorResponse>> AddProductToAuthorList(int id, ProductDto entity)
        {
            Serilog.Log.Information("Reached AddProductToAuthor Endpoint");

            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role.Value == "Manager")
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

            return Unauthorized(new AddProductToAuthorResponse
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "თქვენ არ გაქვთ წვდომა ამ მოქმედებაზე",
                ActionDate = DateTime.Now,
                ProductId = id
            });
        }

        [HttpPatch("UpdateAuthor")]
        public async Task<ActionResult<UpdateAuthorResponse>> UpdateAuthor(AuthorDto entity, int id)
        {
            Serilog.Log.Information("Reached UpdateAuthor Endpoint");
            return await _authorService.UpdateAuthor(entity, id);
        }

        [HttpGet("GetFilteredAuthors")]
        public async Task<ActionResult<GetAuthorsWithFiltersResponse>> GetFilteredAuthors([FromQuery] string nameFilter, string lastnameFilter, string genderFilter, int page, int pageSize, string privateNumber)
        {
            Serilog.Log.Information("Reached GetFilteredAuthors Endpoint");

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
