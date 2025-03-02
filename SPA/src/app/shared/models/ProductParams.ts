export class ProductParams {
  brands: string[] = [];
  types: string[] = [];
  sort: string = 'name';
  pageNumber = 1;
  pageSize = 8;
  search: string = '';
}
