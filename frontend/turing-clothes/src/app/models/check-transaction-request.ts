export interface CheckTransactionRequest {
  hash: string;
  temporaryOrderId: number;
  wallet: string;
  paymentMethod: string;
}
