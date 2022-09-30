import { createViewChild } from '@angular/compiler/src/core';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProductBrand } from '../shared/models/IProductBrand';
import { IProductType } from '../shared/models/IProductType';
import { IProduct } from '../shared/models/product';
import { ProductsParams } from './models/products.params';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  totalCount: number;
  brands: IProductBrand[];
  types: IProductType[];
  sortTypeOptions = [
    { name: "Alphabetical", value: "name" },
    { name: "Price: Low to High", value: "priceAsc" },
    { name: "Price: High to Low", value: "priceDesc" },
  ];
  productsParams = new ProductsParams();

  @ViewChild("search") searchRef: ElementRef;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.productsParams).subscribe(response => {
      this.products = response.data;
      this.totalCount = response.totalCount;
      this.productsParams.pageNumber = response.pageIndex;
      this.productsParams.pageSize = response.pageSize;
    }, error => {
      console.log(error);
    });
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe(response => {
      this.brands = [{ id: 0, name: "All" }, ...response];
    }, error => {
      console.log(error);
    });
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(response => {
      this.types = [{ id: 0, name: "All" }, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    this.productsParams.brandId = brandId;
    this.productsParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.productsParams.typeId = typeId;
    this.productsParams.pageNumber = 1;
    this.getProducts();
  }

  onSortTypeSelected(sort: string) {
    this.productsParams.sortType = sort;
    this.getProducts();
  }

  onPageChanged(pageNumber: number) {
    console.log("this.productsParams.pageNumber = " + this.productsParams.pageNumber + ", pageNumber = " + pageNumber);
    if (pageNumber === this.productsParams.pageNumber)
      return;
    
    this.productsParams.pageNumber = pageNumber;
    this.getProducts();
  }

  onSearch() {
    this.productsParams.search = this.searchRef.nativeElement.value;
    this.getProducts();
  }

  onSearchReset() {
    this.searchRef.nativeElement.value = "";
    this.productsParams = new ProductsParams();
    this.getProducts();
  }
}
