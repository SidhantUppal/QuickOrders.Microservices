export interface CartItemDto {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
  total: number;
  productImage: string;
}

export interface CartDto {
  userId: string;
  items: CartItemDto[];
  totalAmount: number;
}

export interface Response<T> {
  message: string;
  status: boolean;
  data: T;
}

export interface CartRequest {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
}
