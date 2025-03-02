import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { ProductParams } from '../../shared/models/ProductParams';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatSuffix } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
@Component({
  selector: 'app-shop',
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatMenuTrigger,
    MatSelectionList,
    MatListOption,
    MatPaginator,
    FormsModule,
    MatFormField,
    MatInput,
    MatSuffix,
  ],
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css'],
})
export class ShopComponent implements OnInit {
  @ViewChild(MatPaginator) paginator?: MatPaginator;
  private productService = inject(ProductService);
  private dialogService = inject(MatDialog);
  products?: Pagination<Product>;
  productParams: ProductParams = new ProductParams();
  selectedSort: string = 'name';
  sortOptions = [
    { value: 'name', label: 'Alphabetical' },
    { value: 'priceAsc', label: 'Price: Low-High' },
    { value: 'priceDesc', label: 'Price: High-Low' },
  ];
  pageSizeOptions = [4, 8, 12, 16];
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
      next: (response) => (this.products = response),
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
          this.productParams.pageNumber = 1;
          this.getProducts();
        }
      },
    });
  }
  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.productParams.sort = selectedOption.value;
      this.productParams.pageNumber = 1;
      this.getProducts();
    }
  }
  onPageChanged(event: PageEvent) {
    if (event.pageSize !== this.productParams.pageSize) {
      this.productParams.pageSize = event.pageSize;
      this.paginator?.firstPage();
    } else {
      this.productParams.pageNumber = event.pageIndex + 1;
      this.productParams.pageSize = event.pageSize;
    }
    this.getProducts();
  }

  onSearchChange() {
    this.productParams.pageNumber = 1;
    this.getProducts();
  }
}
