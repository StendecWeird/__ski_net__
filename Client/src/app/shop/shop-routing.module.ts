import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';
import { RouterModule } from '@angular/router';

const Routes = [
  { path: "", component: ShopComponent },
  { path: ":id", component: ProductDetailsComponent },
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(Routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
