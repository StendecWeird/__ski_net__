using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => Ok(await _productRepo.ListAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct(int id) => await _productRepo.GetByIdAsync(id);

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands() => Ok(await _productBrandRepo.ListAllAsync());
        
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes() => Ok(await _productTypeRepo.ListAllAsync());

    }
}