export interface PagedResponse<T> {
  success: boolean;
  message: string;
  errors: string[];
  data: T[];
  page: number;
  pageSize: number;
  total: number;
  pages: number;
}
