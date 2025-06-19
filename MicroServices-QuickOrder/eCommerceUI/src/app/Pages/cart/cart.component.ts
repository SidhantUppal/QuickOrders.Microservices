import { Component } from '@angular/core'; // Import Component decorator
import { CartService } from '../../Core/Services/Cart/cart.service'; // Import CartService for cart operations
import { CartDto } from '../../Core/Services/Models/cart.model'; // Import CartDto model
import { CommonModule } from '@angular/common'; // Import CommonModule for Angular common directives

@Component({ // Define the CartComponent as an Angular component
  selector: 'app-cart', // Component selector
  standalone: true, // Standalone component
  imports: [CommonModule], // Import required modules
  templateUrl: './cart.component.html', // Template file
  styleUrl: './cart.component.css' // Styles file
})
export class CartComponent { // Define the CartComponent class


  cart: CartDto | null = null; // Holds the user's cart data, null if not loaded
  errorMessage: string = ''; // Holds error messages for display
  isLoading = true; // Flag to indicate loading state

  constructor(private cartService: CartService) {} // Inject CartService into the component

  async ngOnInit(): Promise<void> { // Lifecycle hook: called on component initialization
    try {
      const user = JSON.parse(sessionStorage.getItem('user') || '{}'); // Get user data from session storage
      const userId = user?.id; // Extract user ID if available
      console.log('User data from sessionStorage:', user); // Log user data for debugging
      console.log('User ID:', userId); // Log user ID for debugging
      if (!userId) { // If user is not logged in
        this.errorMessage = 'User not logged in.'; // Set error message
        this.isLoading = false; // Set loading to false
        return; // Exit the function
      }

      const res = await this.cartService.getUserCart(userId); // Fetch user's cart from CartService
      console.log('Cart response:', res); // Log cart response for debugging
      if (res.status && res.data) { // If response is successful and data exists
        this.cart = res.data; // Assign cart data
      } else {
        this.errorMessage = res.message || 'Failed to load cart.'; // Set error message from response
      }
    } catch (error) { // Handle errors during fetch
      this.errorMessage = (error as Error).message; // Set error message from exception
    } finally {
      this.isLoading = false; // Set loading to false after operation
    }
  }

}
