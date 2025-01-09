import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { CartServiceService } from '../../services/cart-service.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  constructor(private cartService: CartServiceService) {}
  async ngOnInit(): Promise<void> {
    await this.cartService.getCart()
  }

}
