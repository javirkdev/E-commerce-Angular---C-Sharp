import { Injectable, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Cart } from '../models/cart';
import { Result } from '../models/result';
import { CartDetail } from '../models/cart-detail';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { TemporaryOrder } from '../models/temporary-order';

@Injectable({
  providedIn: 'root',
})
export class CartServiceService implements OnInit {
  cartDetail: CartDetail[];
  private cartQuantitySubject = new BehaviorSubject<number>(0);
  cartQuantity$ = this.cartQuantitySubject.asObservable();

  constructor(
    private api: ApiService,
    private authService: AuthService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getCart();
  }

  async getCart(): Promise<Result<Cart>> {
    const result = await this.api.get<Cart>(`Cart/GetCart`);
    //this.cartQuantity = result.data.details.reduce((sum, detail) => sum + detail.amount, 0);
    const quantity = result.data.details.reduce(
      (sum, detail) => sum + detail.amount,
      0
    );
    this.cartQuantitySubject.next(quantity);
    return result;
  }

  async updateQuantity(
    productId: number,
    amount: number
  ): Promise<Result<string>> {
    const result = await this.api.post<string>(
      `Cart/UpdateItem?id=${productId}&amount=${amount}`
    );

    return result;
  }

  async removeProduct(productId: number): Promise<Result<string>> {
    const result = await this.api.delete<string>(
      `Cart/RemoveItem?id=${productId}`
    );
    await this.getCart();
    return result;
  }

  async addProductToCart(
    productId: number,
    quantity: number
  ): Promise<Result<string>> {
    const result = await this.api.put<string>(
      `Cart/AddItem?id=${productId}&quantity=${quantity}`
    );
    return result;
  }

  async syncCarts() {
    
      const localCart: Cart = JSON.parse(localStorage.getItem('localCart'));
      if (!localCart) {
        return;
      }
      localCart.details.forEach((element) => {
        this.updateQuantity(element.productId, element.amount);
      });
      localStorage.removeItem('localCart');
    
  }

  async goToCheckout(cart: Cart) {
    // if (this.authService.isLoggedIn) {
    // const orderDetailDto = cart.details.map((detail) => ({
    //   productId: detail.productId,
    //   amount: detail.amount,
    // }));
    // const result = await this.api.post<TemporaryOrder>(
    //   `TemporaryOrder/CreateTemporaryOrder`,
    //   orderDetailDto
    // );
    // this.router.navigate(['/checkout'], {queryParams: {temporaryId: result.data.id, payment: 'card'}})
    // return result;
    // }
    // const localCart: Cart = JSON.parse(localStorage.getItem('localCart'));
    // const orderDetailDto = localCart.details.map((detail) => ({
    //   productId: detail.productId,
    //   amount: detail.amount,
    // }));
    // const result = await this.api.post<CartDetail>(
    //   `Order/CreateTemporaryOrder`,
    //   orderDetailDto
    // );
    // return result;
  }
}
