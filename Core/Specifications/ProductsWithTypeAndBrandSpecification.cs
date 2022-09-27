using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecification : SpecificationBase<Product>
    {
        public ProductsWithTypeAndBrandSpecification(ProductsRequestOptions requestOptions)
         : base(x => 
            (!requestOptions.TypeId.HasValue || x.ProductTypeId == requestOptions.TypeId) &&
            (!requestOptions.BrandId.HasValue || x.ProductBrandId == requestOptions.BrandId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            AddPagination(requestOptions.PageSize * (requestOptions.PageIndex - 1), requestOptions.PageSize);

            switch(requestOptions.Sort)
            {
                case "priceAsc":
                    SetOrderBy(p => p.Price);
                    break;

                case "priceDesc":
                    SetOrderByDescending(p => p.Price);
                    break;

                default:
                    SetOrderBy(p => p.Name);
                    break;
            }
        }

        public ProductsWithTypeAndBrandSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}