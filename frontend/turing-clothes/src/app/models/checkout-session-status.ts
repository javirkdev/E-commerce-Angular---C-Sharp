import { Order } from "./order";


export interface CheckoutSessionStatus {
    status: string;
    paymentStatus: string;
    customerEmail: string;
    order: Order;
}
