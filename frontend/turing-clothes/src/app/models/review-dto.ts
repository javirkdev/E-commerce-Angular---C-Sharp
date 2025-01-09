import { ProductDto } from "./product-dto";
import { User } from "./user";

export interface ReviewDto {
    id: number;
    productId: number;
    userId: string;
    texto: string;
    dateTime: string;
    rating: number;
    user: User;
}
