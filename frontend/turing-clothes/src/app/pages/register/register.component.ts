import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RegisterDto } from '../../models/register-dto';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { CartServiceService } from '../../services/cart-service.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  myForm: FormGroup;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private cartService: CartServiceService
  ) {
    this.myForm = this.createForm();
  }

  registerHints: Boolean = false;

  private createForm(): FormGroup {
    return this.formBuilder.group(
      {
        name: ['', Validators.required],
        surname: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
        address: ['', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPasswordControl = form.get('confirmPassword');
    const confirmPassword = confirmPasswordControl?.value;

    if (password !== confirmPassword) {
      confirmPasswordControl.setErrors({ mismatch: true });
    }
  }

  async submit() {
    const authData: RegisterDto = {
      name: this.myForm.get('name').value,
      surname: this.myForm.get('surname').value,
      email: this.myForm.get('email').value,
      password: this.myForm.get('password').value,
      address: this.myForm.get('address').value,
    };

    if (this.myForm.valid) {
      const result = await this.authService.register(authData);
      if (result.success) {
        Swal.fire({
          icon: 'success',
          text: 'Registro Correcto',
          showConfirmButton: false,
          animation: true,
          toast: true,
          position: 'top-right',
          timer: 1100
        });
        await this.cartService.syncCarts()
        this.router.navigate(['/home']);
      }
    } else {
      // El formulario no es válido
      this.registerHints = true;
      Swal.fire({
        icon: 'error',
        text: 'Registro erroneo.',
        showConfirmButton: false,
        animation: true,
        toast: true,
        position: 'top-right',
        timer: 1100
      });
    }
  }
}

