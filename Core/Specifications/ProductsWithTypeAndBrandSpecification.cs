using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecification : SpecificationBase<Product>
    {
        public ProductsWithTypeAndBrandSpecification(string? sort, int? typeId, int? brandId)
         : base(x => 
            (!typeId.HasValue || x.ProductTypeId == typeId) &&
            (!brandId.HasValue || x.ProductBrandId == brandId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch(sort)
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