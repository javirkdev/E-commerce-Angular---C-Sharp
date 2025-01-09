import { ProductDto } from "./product-dto";

export interface CartDetail {
  id: number,
  cartId?: number,
  product: ProductDto,
  productId: number,
  amount: number
}
