import { Order } from "./order";

export interface UserDto {
  name : string;
    surname: string;
    email : string;
    password : string;
    address : string;
    role : string;
    orders: Order[];
}
