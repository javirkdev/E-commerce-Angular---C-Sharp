import { CartDetail } from "./cart-detail";

export interface Cart {
  id?: number,
  userId: number,
  details: CartDetail[]
}
