import { TemporaryOrder } from "./temporary-order";

export interface PurchaseInfoDto {
  temporaryOrder: TemporaryOrder;
  totalPrice: number;
  ethereumPrice: string;
  priceInWei: string;
}
