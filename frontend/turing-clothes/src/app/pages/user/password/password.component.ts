import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { UserService } from '../../../services/user.service';
import { PassDto } from '../../../models/pass-dto';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-password',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './password.component.html',
  styleUrl: './password.component.css'
})
export class PasswordComponent {

  passForm: FormGroup;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.passForm = this.createPass();
  }

  registerHints: Boolean = false;

  private createPass(): FormGroup {
    return this.formBuilder.group(
      {
        password: ['', [Validators.required, Validators.minLength(6)]],
        newpassword: ['', [Validators.required, Validators.minLength(6)]],
        checkpass: ['', Validators.required],

      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('newpassword')?.value;
    const confirmPasswordControl = form.get('checkpass');
    const confirmPassword = confirmPasswordControl?.value;

    if (password !== confirmPassword) {
      confirmPasswordControl.setErrors({ mismatch: true });
    }
  }

  async submit() {
    const passDto: PassDto = {
      oldPassword: this.passForm.get('password').value,
      password: this.passForm.get('newpassword').value,
    };
    if (this.passForm.valid) {
      const result = await this.userService.updatePass(passDto);
      if (result.success) {
        Swal.fire({
          icon: 'success',
          text: 'Contraseña modificada.',
          showConfirmButton: false,
          animation: true,
          toast: true,
          position: 'top-right',
          timer: 1100
        });
        this.authService.logout();
        this.router.navigate(['/login']);
      }
    } else {
      // El formulario no es válido
      this.registerHints = true;
      Swal.fire({
        icon: 'error',
        text: 'Las contraseñas no son válidas.',
        showConfirmButton: false,
        animation: true,
        toast: true,
        position: 'top-right',
        timer: 1100
      });
    }
  }


}
