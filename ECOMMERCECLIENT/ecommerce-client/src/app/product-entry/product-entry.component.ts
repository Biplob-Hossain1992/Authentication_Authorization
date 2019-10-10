import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Product } from '../Models/product.model';


@Component({
  selector: 'product-entry',
  templateUrl: './product-entry.component.html',
  styleUrls: ['./product-entry.component.css']
})
export class ProductEntryComponent implements OnInit {

  constructor() {
    this.onSaved = new EventEmitter<Product>();
   }

  ngOnInit() {
    
  }

  @Output() onSaved: EventEmitter<Product>;

  product: Product = new Product() ;
  message: string;

  public save(){
    this.message = "Saved Successfully";

    if(this.onSaved!=null){
      this.onSaved.emit(this.product);
    }
  }

}
