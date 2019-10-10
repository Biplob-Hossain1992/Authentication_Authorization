import { Component } from '@angular/core';
import { Product } from './Models/product.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ecommerce-client';
  

  product: Product = new Product();

  setProduct(product:Product){
    this.product = product;
  }
}
