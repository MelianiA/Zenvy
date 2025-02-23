import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { ProductParams } from '../../shared/models/ProductParams';
@Component({
  selector: 'app-shop',
  imports: [ProductItemComponent, MatButton, MatIcon],
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css'],
})
export class ShopComponent implements OnInit {
  private productService = inject(ProductService);
  private dialogService = inject(MatDialog);
  products: Product[] = [];
  productParams: ProductParams = new ProductParams();
  ngOnInit(): void {
    this.initializeShop();
  }
  initializeShop() {
    this.productService.getBrands();
    this.productService.getTypes();
    this.getProducts();
  }
  getProducts() {
    this.productService.getProducts(this.productParams).subscribe({
      next: (response) => (this.products = response.data),
      error: (error) => console.log(error),
    });
  }
  openFiltersDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.productParams.brands,
        selectedTypes: this.productParams.types,
      },
    });
    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          this.productParams.brands = result.selectedBrands;
          this.productParams.types = result.selectedTypes;
          this.getProducts();
        }
      },
    });
  }
}
