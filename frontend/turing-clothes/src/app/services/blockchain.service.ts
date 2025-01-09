import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { PurchaseInfoDto } from '../models/purchase-info-dto';
import { Result } from '../models/result';
import { CreateEthTransactionRequest } from '../models/create-eth-transaction-request';
import { EthereumInfo } from '../models/ethereum-info';
import { CheckTransactionRequest } from '../models/check-transaction-request';

@Injectable({
  providedIn: 'root'
})
export class BlockchainService {

  constructor(private api: ApiService) { }

  // getContractInfo(nodeUrl: string, contractAddress: string): Promise<Result<Erc20Contract>> {
  //   return this.api.get<Erc20Contract>(`blockchain`, { nodeUrl: nodeUrl, contractAddress: contractAddress })
  // }

  createEthTransaction(data: CreateEthTransactionRequest): Promise<Result<EthereumInfo>> {
    return this.api.post<EthereumInfo>(`Blockchain/transaction`, data)
  }

  checkTransaction(data: CheckTransactionRequest): Promise<Result<boolean>> {
    return this.api.post<boolean>(`Blockchain/check`, data)
  }

  async getEthereumPrice(temporaryOrderId: number) {
    const result = await this.api.get<PurchaseInfoDto>(`Blockchain/GetEthPrice?id=${temporaryOrderId}`);
    if (result.success) {
      return result;
    }
    return result;
  }
}
