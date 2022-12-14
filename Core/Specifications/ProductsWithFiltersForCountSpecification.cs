using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : SpecificationBase<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductsRequestOptions requestOptions)
        : base(x =>
            (string.IsNullOrEmpty(requestOptions.Search) || x.Name.ToLower().Contains(requestOptions.Search)) &&
            (!requestOptions.TypeId.HasValue || x.ProductTypeId == requestOptions.TypeId) &&
            (!requestOptions.BrandId.HasValue || x.ProductBrandId == requestOptions.BrandId))
        {
        }
    }
}