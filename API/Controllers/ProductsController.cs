using System.Net;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductsRequestOptions requestOptions)
        {
            var spec = new ProductsWithTypeAndBrandSpecification(requestOptions);
            var countSpec = new ProductsWithFiltersForCountSpecification(requestOptions);

            var products = await _productRepo.ListAsync(spec);
            var productsDtos = _mapper.Map<IReadOnlyList<Product>, IEnumerable<ProductToReturnDto>>(products);

            var totalCount = await _productRepo.CountAsync(countSpec);

            var pagination = new Pagination<ProductToReturnDto>(
                requestOptions.PageIndex,
                requestOptions.PageSize,
                totalCount,
                productsDtos);

            return Ok(pagination);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypeAndBrandSpecification(id);
            var product = await _productRepo.GetAsync(spec);
            if (product == null)
                return NotFound(new ApiErrorResponse(HttpStatusCode.NotFound));
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands() => Ok(await _productBrandRepo.ListAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes() => Ok(await _productTypeRepo.ListAllAsync());

    }
}