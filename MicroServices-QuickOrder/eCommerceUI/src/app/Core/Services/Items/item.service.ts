// src/app/Core/Services/item.service.ts
import { HttpClient } from '@angular/common/http'; // Import HttpClient for HTTP requests
import { Injectable } from '@angular/core'; // Import Injectable decorator for dependency injection
import { firstValueFrom } from 'rxjs'; // Import firstValueFrom to convert Observable to Promise
import { environment } from '../../../../environments/environment'; // Import environment configuration
import { ApiResponse } from '../Models/api-response.model'; // Import ApiResponse model
import { Item } from '../Models/item.model'; // Import Item model

@Injectable({ // Mark this service as injectable and provided in root
  providedIn: 'root' // This service is available application-wide
})
export class ItemService { // Define the ItemService class
  private readonly baseUrl = `${environment.apigatewayUrl}/gateway/items`; // Base URL for item-related API endpoints

  constructor(private http: HttpClient) {} // Inject HttpClient into the service

  // Get all items
  async getItems(): Promise<ApiResponse<Item[]>> { // Fetch all items from the API
    try {
      return await firstValueFrom(
        this.http.get<ApiResponse<Item[]>>(`${this.baseUrl}/getallitems`) // Make GET request to fetch all items
      );
    } catch { // Handle any errors during the HTTP request
      throw new Error('Failed to fetch items. Please try again later.'); // Throw a user-friendly error
    }
  }

  // Get item by ID
  async getItemById(id: number): Promise<ApiResponse<Item>> { // Fetch a single item by its ID
    try {
      return await firstValueFrom(
        this.http.get<ApiResponse<Item>>(`${this.baseUrl}/getitem/${id}`) // Make GET request to fetch item by ID
      );
    } catch { // Handle any errors during the HTTP request
      throw new Error('Failed to fetch item details. Please try again.'); // Throw a user-friendly error
    }
  }
}
