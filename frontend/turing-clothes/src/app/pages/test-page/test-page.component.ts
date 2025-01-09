import { Component } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-test-page',
  standalone: true,
  imports: [],
  templateUrl: './test-page.component.html',
  styleUrl: './test-page.component.css'
})
export class TestPageComponent {
  secretMessage: string;
  constructor(private AuthService: AuthService) {}

  async obtenerSecreto() {
    const result = await this.AuthService.getSecretMessage()
    if (result.success) {
      this.secretMessage = result.data;
    } else {
      console.error('Error fetching secret message:', result.message);
      this.secretMessage = 'Error fetching secret message';
    }
    console.log(result)
  }
}
