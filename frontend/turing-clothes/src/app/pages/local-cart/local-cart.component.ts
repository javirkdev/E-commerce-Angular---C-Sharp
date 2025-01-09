import { Component, OnInit } from '@angular/core';
import { Cart } from '../../models/cart';
import { FormsModule } from '@angular/forms';
import { CartDetail } from '../../models/cart-detail';
import { CatalogService } from '../../services/catalog.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-local-cart',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './local-cart.component.html',
  styleUrl: './local-cart.component.css',
})
export class LocalCartComponent implements OnInit {
  cart: Cart;

  constructor(private catalogService: CatalogService, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.cart = this.getLocalCart();
  }

  getLocalCart(): Cart {
    return JSON.parse(localStorage.getItem('localCart'));
  }

  saveLocal() {
    localStorage.setItem('localCart', JSON.stringify(this.cart));
  }

  removeProduct(productId: number) {
    this.cart.details = this.cart.details.filter(
      (detail) => detail.productId !== productId
    );

    this.saveLocal();
    this.getLocalCart();
  }

  async increaseQuantity(productId: number) {
    const product = (await this.catalogService.getProductDetailsById(productId))
      .data;
    let cartDetail = this.cart.details.find(
      (detail) => detail.productId === productId
    );
    if (cartDetail.amount < product.stock) {
      cartDetail.amount++;
      this.updateQuantity(productId, cartDetail.amount);
      this.saveLocal();
    }
  }

  async updateQuantity(productId: number, amount: number) {
    const localCart: Cart = JSON.parse(localStorage.getItem('localCart'));

    const product = (await this.catalogService.getProductDetailsById(productId))
      .data;

    let cartDetail = this.cart.details.find(
      (detail) => detail.productId === productId
    );
    if (localCart == null) {
      return false;
    }

    if (cartDetail == null) {
      if (amount > 0) {
        const newDetails: CartDetail = {
          id: null,
          cartId: null,
          productId: productId,
          product: product,
          amount: amount,
        };
        this.cart.details.push(newDetails);
      }
    } else {
      switch (true) {
        case amount === 0:
          this.cart.details = this.cart.details.filter(
            (detail) => detail.productId !== productId
          );
          break;

        case amount > product.stock:
          cartDetail.amount = product.stock;
          break;

        default:
          cartDetail.amount = amount;
          break;
      }
    }

    this.saveLocal();
    return true;
  }

  async decreaseQuantity(productId: number) {
    const product = (await this.catalogService.getProductDetailsById(productId))
      .data;
    let cartDetail = this.cart.details.find(
      (detail) => detail.productId === productId
    );
    if (cartDetail.amount < product.stock) {
      cartDetail.amount--;
      this.updateQuantity(productId, cartDetail.amount);
      this.saveLocal();
    }
  }

  getTotal() {
    return this.cart.details.reduce(
      (sum, item) => sum + item.product.price * item.amount,
      0
    );
  }


  async goToCheckout() {
    
    if (!this.authService.isLoggedIn) {
      await this.router.navigate(['/login']);
    }
  }

  async goToBlockchainCheckout() {
    if (!this.authService.isLoggedIn) {
      await this.router.navigate(['/login'])
    }
  }
}
