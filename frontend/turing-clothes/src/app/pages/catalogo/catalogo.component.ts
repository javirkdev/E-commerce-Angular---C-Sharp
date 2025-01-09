import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../models/product-dto';
import { CatalogService } from '../../services/catalog.service';
import { CommonModule } from '@angular/common';
import { PaginationParams } from '../../models/pagination-params';
import { PagedResults } from '../../models/paged-results';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css',
})
export class CatalogoComponent implements OnInit {
  productList: ProductDto[] = [];
  imageUrl: string[];
  items: ProductDto[] = [];
  isAscending: boolean = true;
  oldQuery: string;
  paginationParams: PaginationParams;
  pagedResults: PagedResults;
  productReviews: { [key: number]: number } = {};

  constructor(private catalogService: CatalogService) {}

  async ngOnInit() {
    const savedSettings = this.catalogService.getUserSettings();

    this.paginationParams = savedSettings;
    this.oldQuery = this.paginationParams.query;

    await this.getPagedResults();
    this.isAscending = this.paginationParams.direction === 0;
  }

  async getPagedResults() {
    if (this.oldQuery !== this.paginationParams.query) {
      this.paginationParams.pageNumber = 1;
    }

    this.oldQuery = this.paginationParams.query;

    const result = await this.catalogService.getPagedResults(
      this.paginationParams
    );

    if (result.success) {
      this.pagedResults = result.data;
      this.paginationParams.pageNumber = result.data.pageNumber;
      this.items = this.pagedResults.results;
      this.items = result.data.results.map((product) => ({
        ...product,
        image: `${environment.imageUrl}${product.image}`,
      }));
      for (const item of this.items) {
        item.averageRating = await this.getAverageRating(item.id);
      }
    }
  }
  catch(error) {
    console.error('Error al obtener los productos:', error);
  }

  async getAverageRating(productId: number): Promise<number> {
    const averageRating = await this.catalogService.getAverageRating(productId);
    return averageRating;
  }

  /**
   * Ordenar por precio: 0.
   * Ordenar por nombre: 1.
   * Ordenar por ascendente: 0.
   * Ordenar por descendente: 1.
   */

  async setOrderBy(choice: number) {
    this.paginationParams.orderBy = choice;
    this.paginationParams.pageNumber = 1;
    await this.getPagedResults();
  }

  round(value: number): number {
    return Math.round(value);
  }

  async toggleDirection() {
    this.paginationParams.direction =
      this.paginationParams.direction === 0 ? 1 : 0;
    this.isAscending = this.paginationParams.direction === 0;
    this.paginationParams.pageNumber = 1;
    await this.getPagedResults();
  }

  async nextPage() {
    if (
      this.paginationParams.pageNumber < this.pagedResults.totalNumberOfPages
    ) {
      this.paginationParams.pageNumber++;
      await this.getPagedResults();
    }
  }

  async previousPage() {
    if (this.paginationParams.pageNumber > 1) {
      this.paginationParams.pageNumber--;
      await this.getPagedResults();
    }
  }

  getPageNumbers(): number[] {
    return Array.from(
      { length: this.pagedResults?.totalNumberOfPages },
      (_, i) => i + 1
    );
  }

  async goToPage(page: number) {
    if (page !== this.paginationParams.pageNumber) {
      this.paginationParams.pageNumber = page;
      await this.getPagedResults();
    }
  }

  async onProductsPerPageChange(value: number) {
    this.paginationParams.pageSize = value;
    await this.getPagedResults();
  }
}
