import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { ProductParams } from '../../shared/models/ProductParams';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  brands: string[] = [];
  types: string[] = [];
  getProducts(productParams: ProductParams) {
    let params = new HttpParams();
    if (productParams.brands.length > 0)
      params = params.append('brands', productParams.brands.join(','));
    if (productParams.types.length > 0)
      params = params.append('types', productParams.types.join(','));
    if (productParams.sort) params = params.append('sort', productParams.sort);
    if (productParams.search)
      params = params.append('search', productParams.search);

    params = params.append('pageSize', productParams.pageSize.toString());
    params = params.append('pageIndex', productParams.pageNumber.toString());
    return this.http.get<Pagination<Product>>(this.baseUrl + 'products', {
      params,
    });
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/brands').subscribe({
      next: (response) => (this.brands = response),
      error: (error) => console.log(error),
    });
  }
  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/types').subscribe({
      next: (response) => (this.types = response),
      error: (error) => console.log(error),
    });
  }
}
