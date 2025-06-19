import { Component } from '@angular/core'; // Import Component decorator
import { ItemService } from '../../Core/Services/Items/item.service'; // Import ItemService for fetching items
import { ApiResponse } from '../../Core/Services/Models/api-response.model'; // Import ApiResponse model
import { Item } from '../../Core/Services/Models/item.model'; // Import Item model
import Swal from 'sweetalert2'; // Import SweetAlert2 for alerts
import { CommonModule } from '@angular/common'; // Import CommonModule for Angular common directives
import { FormsModule } from '@angular/forms'; // Import FormsModule for template-driven forms
import { RouterLink, Router } from '@angular/router'; // Import RouterLink and Router for navigation
import { CartService } from '../../Core/Services/Cart/cart.service'; // Import CartService for cart operations
import { CartRequest } from '../../Core/Services/Models/cart.model'; // Import CartRequest model


@Component({ // Define the HomeComponent as an Angular component
  selector: 'app-home', // Component selector
  standalone: true, // Standalone component
  imports: [CommonModule, FormsModule, RouterLink], // Import required modules
  templateUrl: './home.component.html', // Template file
  styleUrl: './home.component.css' // Styles file
})
export class HomeComponent { // Define the HomeComponent class
  items: Item[] = []; // Array to hold items for display

  constructor(
    private itemService: ItemService, // Inject ItemService
    private cartService: CartService, // Inject CartService
    private router: Router // Inject Router for navigation
  ) {}

  async ngOnInit(): Promise<void> { // Lifecycle hook: called on component initialization
    await this.loadItems(); // Load items when component initializes
  }

  private async loadItems(): Promise<void> { // Method to load items from the API
    try {
      const response: ApiResponse<Item[]> = await this.itemService.getItems(); // Fetch items from ItemService
      if (response.status) { // If response is successful
        this.items = response.data; // Assign items to the array
      } else {
        Swal.fire('Oops!', response.message, 'warning'); // Show warning if response is not successful
      }
    } catch (error: any) { // Handle errors during fetch
      Swal.fire('Error', error.message || 'Unable to load items.', 'error'); // Show error alert
    }
  }

  async addToCart(item: Item): Promise<void> { // Method to add an item to the cart
    const user = sessionStorage.getItem('user'); // Get user from session storage
    if (!user) { // If user is not logged in
      Swal.fire('Login Required', 'Please login to add items to your cart.', 'info'); // Show login required alert
      this.router.navigate(['/login']); // Redirect to login page
      return; // Exit the function
    }

    const userId = JSON.parse(user).id; // Parse user ID from session storage

    const cartRequest: CartRequest = { // Create a CartRequest object
      productId: item.id, // Set product ID
      productName: item.name, // Set product name
      price: item.price, // Set product price
      quantity: 1, // Default quantity to 1
    };

    try {
      const response = await this.cartService.addToCart(userId, cartRequest); // Call CartService to add item
      if (response.status) { // If add to cart is successful
        Swal.fire('Success', 'Item added to cart.', 'success'); // Show success alert
      } else {
        Swal.fire('Failed', response.message, 'error'); // Show failure alert
      }
    } catch (error: any) { // Handle errors during add to cart
      Swal.fire('Error', error.message || 'Unable to add item.', 'error'); // Show error alert
    }
  }
}





// import { Component } from '@angular/core';
// import { ItemService } from '../../Core/Services/Items/item.service';
// import { ApiResponse } from '../../Core/Services/Models/api-response.model';
// import { Item } from '../../Core/Services/Models/item.model';
// import Swal from 'sweetalert2';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { RouterLink } from '@angular/router';


// @Component({
//   selector: 'app-home',
//   standalone: true,
//   imports: [CommonModule, FormsModule, RouterLink],
//   templateUrl: './home.component.html',
//   styleUrl: './home.component.css'
// })
// export class HomeComponent {

// items: Item[] = [];

//   constructor(private itemService: ItemService) {}

//   async ngOnInit(): Promise<void> {
//     await this.loadItems();
//   }

//   private async loadItems(): Promise<void> {
//     try {
//       const response: ApiResponse<Item[]> = await this.itemService.getItems();
//       if (response.status) {
//         this.items = response.data;
//       } else {
//         Swal.fire('Oops!', response.message, 'warning');
//       }
//     } catch (error: any) {
//       Swal.fire('Error', error.message || 'Unable to load items.', 'error');
//     }
//   }


// }
