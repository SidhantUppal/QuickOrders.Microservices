import { Component } from '@angular/core'; // Import Component decorator
import { ApiResponse } from '../../Core/Services/Models/api-response.model'; // Import ApiResponse model
import { Order } from '../../Core/Services/Models/order.model'; // Import Order model
import { OrderService } from '../../Core/Services/Orders/order.service'; // Import OrderService for fetching orders
import { CommonModule } from '@angular/common'; // Import CommonModule for Angular common directives

@Component({ // Define the OrdersComponent as an Angular component
  selector: 'app-orders', // Component selector
  standalone: true, // Standalone component
  imports: [CommonModule], // Import required modules
  templateUrl: './orders.component.html', // Template file
  styleUrl: './orders.component.css' // Styles file
})
export class OrdersComponent { // Define the OrdersComponent class


  orders: Order[] = []; // Array to hold user's orders
  isLoading = true; // Flag to indicate loading state
  errorMessage = ''; // String to hold error messages

  constructor(private orderService: OrderService) {} // Inject OrderService into the component

  async ngOnInit(): Promise<void> { // Lifecycle hook: called on component initialization
    const user = sessionStorage.getItem('user'); // Get user from session storage
    const userId = user ? JSON.parse(user).id : null; // Parse user ID if user exists

    if (!userId) { // If user is not logged in
      this.errorMessage = 'Please log in to view your orders.'; // Set error message
      this.isLoading = false; // Set loading to false
      return; // Exit the function
    }

    try {
      const response: ApiResponse<Order[]> = await this.orderService.getUserOrders(userId); // Fetch user's orders from OrderService
      if (response.status) { // If response is successful
        this.orders = response.data; // Assign orders to the array
      } else {
        this.errorMessage = response.message; // Set error message from response
      }
    } catch (err: any) { // Handle errors during fetch
      this.errorMessage = err.message || 'Something went wrong while loading orders.'; // Set error message
    } finally {
      this.isLoading = false; // Set loading to false after operation
    }
  }


}
