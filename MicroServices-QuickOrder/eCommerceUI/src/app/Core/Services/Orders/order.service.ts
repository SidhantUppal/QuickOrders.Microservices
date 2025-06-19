import { HttpClient } from '@angular/common/http'; // Import HttpClient for making HTTP requests
import { Injectable } from '@angular/core'; // Import Injectable decorator for dependency injection
import { firstValueFrom } from 'rxjs'; // Import firstValueFrom to convert Observable to Promise
import { environment } from '../../../../environments/environment'; // Import environment variables
import { ApiResponse } from '../Models/api-response.model'; // Import ApiResponse model
import { Order } from '../Models/order.model'; // Import Order model

@Injectable({ // Mark this service as injectable and provided in root
  providedIn: 'root' // This service is available application-wide
})
export class OrderService { // Define the OrderService class

private apiBase = `${environment.apigatewayUrl}/gateway/orders`; // Base URL for order-related API endpoints

  constructor(private http: HttpClient) {} // Inject HttpClient into the service

    async getUserOrders(userId: string): Promise<ApiResponse<Order[]>> { // Fetch orders for a specific user
    try {
      const url = `${this.apiBase}/getuserorder/${userId}`; // Construct the API endpoint URL
      // alert(url); // (Commented out) Alert for debugging the URL
      return await firstValueFrom(this.http.get<ApiResponse<Order[]>>(url)); // Make GET request and return the response as a Promise
    } catch (error) { // Handle any errors during the HTTP request
      console.error('Error fetching orders:', error); // Log the error to the console
      throw new Error('Unable to fetch orders at this time.'); // Throw a user-friendly error
    }
  }


}
