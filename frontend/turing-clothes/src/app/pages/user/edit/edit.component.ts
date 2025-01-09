import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { EditDto } from '../../../models/edit-dto';
import { UserService } from '../../../services/user.service';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgIf } from '@angular/common';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent implements OnInit {

  userInfo: EditDto;

  editName: boolean = false;
  editSurname: boolean = false;
  editEmail: boolean = false;
  editAddress: boolean = false;

  constructor(
    private userService: UserService,
    private router: Router,
    private authService: AuthService
  ) {

  }
  ngOnInit(): void {
    this.getUser()
  }


  async getUser(): Promise<void> {
    const result = await this.userService.getEditUser()
    this.userInfo = result.data;
  }

  async saveChanges() {
    const result = await this.userService.editUser(this.userInfo)
    if (result.success) {
      Swal.fire({
        icon: 'success',
        text: 'Usuario actualizado',
        showConfirmButton: false,
        animation: true,
        toast: true,
        position: 'top-right',
        timer: 1100
      });
      this.authService.logout();
      this.router.navigate(['/login']);
    }
    else {
      Swal.fire({
        icon: 'error',
        text: 'Edición erronea.',
        showConfirmButton: false,
        animation: true,
        toast: true,
        position: 'top-right',
        timer: 1100
      });
    }
  }
}
