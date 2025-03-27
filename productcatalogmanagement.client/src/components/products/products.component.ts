import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Product } from '../../models/product.model';
import * as ProductActions from '../../store/product.actions';
import { ProductState } from '../../store/product.reducer';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products$: Observable<Product[]>;
  selectedProduct: Product | null = null;

  constructor(private store: Store<{ products: ProductState }>) {
    this.products$ = store.select(state => state.products.products);
  }

  ngOnInit(): void {
    this.store.dispatch(ProductActions.loadProducts());
  }

  selectProduct(product: Product): void {
    this.selectedProduct = { ...product };
  }

  saveProduct(): void {
    if (this.selectedProduct) {
      if (this.selectedProduct.id) {
        this.store.dispatch(ProductActions.updateProduct({ product: this.selectedProduct }));
      } else {
        this.store.dispatch(ProductActions.addProduct({ product: this.selectedProduct }));
      }
      this.selectedProduct = null;
    }
  }

  deleteProduct(id: number): void {
    this.store.dispatch(ProductActions.deleteProduct({ id }));
  }

  newProduct(): void {
    this.selectedProduct = { id: 0, name: '', price: 0, description: '' };
  }

  cancel(): void {
    this.selectedProduct = null;
  }
}
