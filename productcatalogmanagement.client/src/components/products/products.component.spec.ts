import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductsComponent } from './products.component';
import { StoreModule } from '@ngrx/store';
import { FormsModule } from '@angular/forms';
import { productReducer } from '../../store/product.reducer';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';

describe('ProductsComponent', () => {
  let component: ProductsComponent;
  let fixture: ComponentFixture<ProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductsComponent],
      imports: [
        HttpClientTestingModule,
        FormsModule,
        StoreModule.forRoot({ products: productReducer })
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display product list', () => {
    component.products$ = of([
      { id: 1, name: 'Product 1', price: 10, description: 'Description 1' },
      { id: 2, name: 'Product 2', price: 20, description: 'Description 2' }
    ]);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('li').length).toBe(2);
  });

  it('should select a product', () => {
    const product = { id: 1, name: 'Product 1', price: 10, description: 'Description 1' };
    component.selectProduct(product);
    expect(component.selectedProduct).toEqual(product);
  });

  it('should clear selected product on cancel', () => {
    component.selectedProduct = { id: 1, name: 'Product 1', price: 10, description: 'Description 1' };
    component.cancel();
    expect(component.selectedProduct).toBeNull();
  });
});
