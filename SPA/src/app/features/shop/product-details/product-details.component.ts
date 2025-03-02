import { Component, inject, OnInit } from "@angular/core";
import { ProductService } from "../../../core/services/product.service";
import { ActivatedRoute, RouterLink } from "@angular/router";
import { Product } from "../../../shared/models/product";
import { MatIcon } from "@angular/material/icon";
import { MatButton } from "@angular/material/button";
import { MatDivider } from "@angular/material/divider";
import { MatFormField } from "@angular/material/form-field";
import { MatInput } from "@angular/material/input";
import { PinchZoomComponent } from "@meddv/ngx-pinch-zoom";

@Component({
  selector: "app-product-details",
  imports: [
    MatIcon,
    MatButton,
    MatDivider,
    MatFormField,
    MatInput,
    RouterLink,
    PinchZoomComponent,
  ],
  templateUrl: "./product-details.component.html",
  styleUrl: "./product-details.component.scss",
})
export class ProductDetailsComponent implements OnInit {
  private productService = inject(ProductService);
  private activatedRoute = inject(ActivatedRoute);
  product?: Product;
  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get("id");
    if (id) {
      this.productService.getProduct(+id).subscribe({
        next: (response) => (this.product = response),
        error: (error) => console.log(error),
      });
    }
  }
}
