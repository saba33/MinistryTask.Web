using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.ProductResponseModels;
using System.Security.Claims;

namespace MinistryTask.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpPost("AddProduct")]
        public async Task<ActionResult<AddProductResponseModel>> AddProduct(ProductDto entity)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role.Value == "Admin")
            {
                var result = await _service.AddProductAsync(entity);
                return Ok(result);
            }
            return Unauthorized(new AddProductResponseModel
            {
                Message = "თქვენ არ გაქვთ უფლება ამ მოქმედების შესასრულებლად",
                StatusCode = StatusCodes.Status401Unauthorized,
            });
        }
        [HttpPost("ArchiveProduct")]
        public async Task<ActionResult<ArchiveProductResponse>> ArchiveProduct(int id)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role.Value == "Admin")
            {
                var ProductToInsert = await _service.ArchiveProduct(id);
                return Ok(ProductToInsert);
            }
            return Unauthorized(new ArchiveProductResponse
            {
                Message = "თქვენ არ გაქვთ უფლება ამ მოქმედების შესასრულებლად",
                StatusCode = StatusCodes.Status401Unauthorized,
            });
        }
        [HttpPost("PublishProduct")]
        public async Task<ActionResult<ArchiveProductResponse>> PublishProduct(int id)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role.Value == "Admin")
            {
                var ProductToInsert = await _service.PublishProduct(id);
                return Ok(ProductToInsert);
            }
            return Unauthorized(new ArchiveProductResponse
            {
                Message = "თქვენ არ გაქვთ უფლება ამ მოქმედების შესასრულებლად",
                StatusCode = StatusCodes.Status401Unauthorized,
            });
        }

        [HttpGet("GetProductStatus")]
        public async Task<ActionResult<GetProductStatusResponse>> GetProductStatus(int id)
        {
            var result = await _service.GetProductStatusAsync(id);
            return Ok(result);
        }
    }
}
