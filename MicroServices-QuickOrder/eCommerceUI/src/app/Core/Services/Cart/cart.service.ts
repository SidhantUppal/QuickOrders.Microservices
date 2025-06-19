import { HttpClient } from '@angular/common/http'; // Import HttpClient for making HTTP requests
import { Injectable } from '@angular/core'; // Import Injectable decorator for dependency injection
import { environment } from '../../../../environments/environment'; // Import environment configuration
import { CartDto, CartRequest } from '../Models/cart.model'; // Import CartDto and CartRequest models
import { firstValueFrom } from 'rxjs'; // Import firstValueFrom to convert Observable to Promise
import { ApiResponse } from '../Models/api-response.model'; // Import ApiResponse model

@Injectable({ // Mark this service as injectable and provided in root
  providedIn: 'root' // This service is available application-wide
})
export class CartService { // Define the CartService class


   private apiBase = `${environment.apigatewayUrl}/gatewaycart/cart`; // Base URL for cart-related API endpoints

  constructor(private http: HttpClient) {} // Inject HttpClient into the service



  async getUserCart(userId: string): Promise<ApiResponse<CartDto>> { // Fetch the cart for a specific user
      try {
        return await firstValueFrom(
          this.http.get<ApiResponse<CartDto>>(`${this.apiBase}/${userId}`) // Make GET request to fetch user's cart
        );
      } catch { // Handle any errors during the HTTP request
        throw new Error('Failed to fetch cart. Please try again later.'); // Throw a user-friendly error
      }
    }

  async addToCart(userId: string, request: CartRequest): Promise<ApiResponse<CartDto>> { // Add an item to the user's cart
    try {
      const url = `${this.apiBase}/${userId}`; // Construct the API endpoint URL
      return await firstValueFrom(
        this.http.post<ApiResponse<CartDto>>(url, request) // Make POST request to add item to cart
      );
    } catch (error) { // Handle any errors during the HTTP request
      throw new Error('Failed to add item to cart.'); // Throw a user-friendly error
    }
  }

}
