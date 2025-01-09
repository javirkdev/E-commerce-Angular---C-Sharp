import { Component } from '@angular/core';
import { StripeService } from 'ngx-stripe';
import { BlockchainService } from '../../services/blockchain.service';

@Component({
  selector: 'app-blockchain',
  standalone: true,
  imports: [],
  templateUrl: './blockchain.component.html',
  styleUrl: './blockchain.component.css'
})
export class BlockchainComponent {
  cartItems = [
    { id: 1, name: 'Camiseta', quantity: 2, price: 19.99, image: 'https://localhost:7183/images/Camiseta-cuello-redondo-black.png' },
    { id:2, name: 'Pantalón', quantity: 1, price: 39.99, image: 'https://localhost:7183/images/pantalon-vaquero-hombre.png' },
    { id:3, name: 'Chaqueta', quantity: 1, price: 59.99, image: 'https://localhost:7183/images/cazadoraAnte.png' }
  ];

  constructor(private stripe: StripeService, private blockchainService: BlockchainService) {}

  
  getTotal(): number {
    return this.cartItems.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }

  getTotalEth(): number {
    this.blockchainService.getEthereumPrice(1);
    return 1;
  }

  proceedToPayment(): void {
    alert('Redirigiendo a la pasarela de pago...');
  }
}
