export interface Item {
  id: number;
  name: string;
  price: number;
  description: string;
  productImage: string;
}
export interface ItemResponse {
  message: string;
  status: boolean;
  data: Item[];
}
