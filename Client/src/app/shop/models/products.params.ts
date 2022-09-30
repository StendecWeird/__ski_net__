export class ProductsParams {
    brandId: number = 0;
    typeId: number = 0;
    sortType: string = "name";
    pageNumber: number = 1;
    pageSize: number = 3;
    search: string;
}