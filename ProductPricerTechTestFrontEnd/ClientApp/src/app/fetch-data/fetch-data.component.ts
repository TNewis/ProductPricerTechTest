import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { EditProductRequest, NewProductRequest, Product, ProductsService } from '../Products/products.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent {
  productsService: ProductsService;
  public products: Product[] = [];
  public selectedProduct: Product | undefined;
  public editingProduct: boolean = false;

  textAreaProductName: string ='';
  textAreaProductPrice: string ='';

  constructor(http: HttpClient, productsService: ProductsService) {
    this.productsService = productsService;
    this.productsService.getProducts().subscribe(
      result => { this.products = result; },
      error => console.error(error));
  }


  public addProduct() {
    let newProduct: NewProductRequest = {
      productName: this.textAreaProductName,
      price: +this.textAreaProductPrice
    };
    this.productsService.addProduct(newProduct).subscribe(product => this.products.push(product));
  }

  public editProduct() {
    let editProduct: EditProductRequest = {
      productGuid: this.selectedProduct?.guid ?? '',
      price: +this.textAreaProductPrice
    };

    this.products = this.products.filter(p => p.guid != this.selectedProduct?.guid);
    this.productsService.editProduct(editProduct).subscribe(product => this.products.push(product));
  }

  public selectProduct(product: Product) {
    this.selectedProduct = product;
    //this.textAreaProductName = this.selectedProduct.name;
    this.showEdit();
  }

  public showEdit() {
    this.editingProduct = true;
  }

  public hideEdit() {
    this.editingProduct = false;
  }

  numericOnly(event: { key: string; }): boolean {
    let patt = /^\d*\.?\d{0,2}$/;
    let result = patt.test(this.textAreaProductPrice + event.key);
    return result;
  }
}
