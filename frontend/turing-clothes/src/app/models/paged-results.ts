import { ProductDto } from "./product-dto";

export interface PagedResults {
  pageNumber: number,
  pageSize: number,
  totalNumberOfPages: number,
  totalNumberOfRecords: number,
  results: ProductDto[]
}
