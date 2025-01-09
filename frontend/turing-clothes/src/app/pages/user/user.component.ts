import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user';
import { Order } from '../../models/order';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../models/user-dto';
import { environment } from '../../../environments/environment';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {

user: UserDto;
apiUrl = environment.imageUrl;

constructor(private userService:UserService, private router: Router){}

async ngOnInit(): Promise<void> {
  await this.getOrders();
}

  async getOrders():Promise<void> {
    const result = await this.userService.getUserOrder()
    this.user = result.data;
  }


}
