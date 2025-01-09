import { Component, OnInit } from '@angular/core';
import { SidebarComponent } from '../../../shared/sidebar/sidebar.component';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [SidebarComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css',
})
export class UsersComponent implements OnInit {
  users: any[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.getUsers();
  }
  async getUsers(): Promise<void> {
    const result = await this.adminService.getUsers();
    if (result.success) {
      this.users = result.data;
    }
  }

  async deleteUser(email: string): Promise<void> {
    const result = await this.adminService.deleteUser(email);
    if (result.success) {
      this.getUsers();
    } else {
      alert('Como mínimo debe de haber un administrador en el sistema');
      this.getUsers();
    }
  }

  async changeUserRole(userEmail: string, newRole: string) {
    const newUserRole = await this.adminService.updateUserRole(userEmail, newRole);
    if (newUserRole.success) {
      this.getUsers();
    } else {
      alert('Como mínimo debe de haber un administrador en el sistema');
      this.getUsers();
    }
  }
}
