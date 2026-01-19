export interface TransactionReadDto {
  id: string;
  amount: number;
  description: string;
  date: string;
  categoryName: string;
  categoryIcon: string; 
  type: number;
}

export interface TransactionUpsertDto {
  amount: number;
  description: string;
  date: string;
  type: number;
  categoryId: string;
}
