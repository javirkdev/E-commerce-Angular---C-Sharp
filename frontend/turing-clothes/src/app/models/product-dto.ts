import { ReviewDto } from "./review-dto";

export interface ProductDto {
  id: number;

  name: string;

  description: string;

  price: number;

  stock: number;

  image: string;
  
  reviews: ReviewDto[];

  averageRating?: number;
}
