import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { Result } from '../../models/result';
import { CartServiceService } from '../../services/cart-service.service';
import { Cart } from '../../models/cart';
import { AddReview } from '../../models/add-review';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
})
export class ProductDetailsComponent implements OnInit {
  items: ProductDto[] = [];
  product: ProductDto | null = null;
  reviewText: string = '';
  quantity: number = 1;
  productReviews: { [key: number]: number } = {};

  constructor(
    private activatedRoute: ActivatedRoute,
    private catalogService: CatalogService,
    public authService: AuthService,
    private cartService: CartServiceService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    const id = this.activatedRoute.snapshot.paramMap.get(
      'id'
    ) as unknown as number;

    const result = await this.catalogService.getProductDetailsById(id);

    this.product = result.data;

    this.product.averageRating = await this.getAverageRating(this.product.id);
  }

  async getAverageRating(productId: number): Promise<number> {
    const averageRating = await this.catalogService.getAverageRating(productId);
    return averageRating;
  }

  async confirmReview(): Promise<void> {
    if (!this.product) return;

    const review: AddReview = {
      productId: this.product.id,
      texto: this.reviewText,
    };
    try {
      const result = await this.catalogService.addReview(review);
      if (result.success) {
        Swal.fire('Reseña enviada');
        this.reviewText = '';
        this.product.reviews.push(result.data);
        this.ngOnInit();
      } else {
        Swal.fire('No se pudo enviar la reseña');
      }
    } catch {
      Swal.fire('Error');
    }
  }
  async buyNow(productId: number, quantity: number): Promise<void> {
    if (quantity > 0) {
      this.addProductToCart(productId, quantity);
      if (this.authService.isLoggedIn) {
        await this.router.navigate(['/cart']);
        await this.cartService.getCart();
      } else {
        await this.router.navigate(['/login']);
        alert('Inicia sesión para continuar con tu compra');
      }
    }
  }

  async addProductToCart(
    productId: number,
    quantity: number
  ): Promise<Result<string>> {
    if (!this.authService.isLoggedIn) {
      const localCart: Cart = JSON.parse(localStorage.getItem('localCart')) || {
        details: [],
      };
      const product = (
        await this.catalogService.getProductDetailsById(productId)
      ).data;

      const existingDetail = localCart.details.find(
        (detail) => detail.productId === productId
      );

      if (existingDetail) {
        if (existingDetail.amount >= this.product.stock) {
          alert('No se pueden añadir productos si no hay stock');
        } else {
          existingDetail.amount += quantity;
        }
      } else {
        const newDetails = {
          id: null,
          productId: product.id,
          product,
          amount: quantity,
          cartId: null,
        };
        localCart.details.push(newDetails);
      }
      localStorage.setItem('localCart', JSON.stringify(localCart));
      Swal.fire({
        icon: 'success',
        text: 'Producto agregado al carrito',
        showConfirmButton: false,
        animation: true,
        toast: true,
        position: 'top-right',
        timer: 1100,
      });
      return Result.success(200, 'Producto agregado al carrito');
    }
    const result = await this.cartService.addProductToCart(productId, quantity);
    console.log(result);
    if (result.success) {
      Swal.fire({
        icon: 'success',
        text: 'Producto agregado al carrito',
        showConfirmButton: false,
        animation: true,
        toast: true,
        position: 'top-right',
        timer: 1100,
      });
      await this.cartService.getCart();
      return result;
    }
    return result;
  }
}
