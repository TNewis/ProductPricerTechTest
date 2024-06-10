import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment.prod';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ProductsService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  public addProduct(newProductRequest: NewProductRequest): Observable<Product> {
    return this.http.post<Product>(environment.apiUrl + 'Product', newProductRequest).pipe(catchError(this.handleError));
  }

  public editProduct(editProductRequest: EditProductRequest): Observable<Product> {
    return this.http.put<Product>(environment.apiUrl + 'Product', editProductRequest).pipe(catchError(this.handleError));
  }


  public getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + 'Product').pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }

}

export interface Product {
  name: string;
  price: number;
  currency: string;
  guid: string;
}

export interface NewProductRequest {
  productName: string;
  price: number;
}

export interface EditProductRequest {
  productGuid: string;
  price: number;
}
