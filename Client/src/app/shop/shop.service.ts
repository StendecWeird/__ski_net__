import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProductBrand } from '../shared/models/IProductBrand';
import { IProductType } from '../shared/models/IProductType';
import { IPagination } from '../shared/models/pagination';
import { ProductsParams } from './models/products.params';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = "https://localhost:5001/api/"

  constructor(private http: HttpClient) { }

  getProducts(productsParams: ProductsParams) {
    let params = new HttpParams();
    if (productsParams.brandId > 0) {
      params = params.append("brandId", productsParams.brandId);
    }
    if (productsParams.typeId > 0) {
      params = params.append("typeId", productsParams.typeId);
    }

    params = params.append("sort", productsParams.sortType);

    if (productsParams.pageNumber > 0) {
      params = params.append("pageIndex", productsParams.pageNumber);
    }
    if (productsParams.pageSize > 0) {
      params = params.append("pageSize", productsParams.pageSize);
    }

    if (productsParams.search) {
      params = params.append("search", productsParams.search);
    }

    return this.http.get<IPagination>(this.baseUrl + "products", { params });
  }

  getProductBrands() {
    return this.http.get<IProductBrand[]>(this.baseUrl + "products/brands");
  }

  getProductTypes() {
    return this.http.get<IProductType[]>(this.baseUrl + "products/types");
  }
}
