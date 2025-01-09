import { CartDetail } from "./cart-detail";

export interface TemporaryOrder {
    id: number,
    userId: number,
    details: CartDetail[]
}
