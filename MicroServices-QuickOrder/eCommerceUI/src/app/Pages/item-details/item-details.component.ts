import { Component } from '@angular/core'; // Import Component decorator
import { ActivatedRoute, Router } from '@angular/router'; // Import ActivatedRoute and Router for route handling and navigation
import Swal from 'sweetalert2'; // Import SweetAlert2 for alerts
import { ItemService } from '../../Core/Services/Items/item.service'; // Import ItemService for fetching item details
import { Item } from '../../Core/Services/Models/item.model'; // Import Item model
import { CommonModule } from '@angular/common'; // Import CommonModule for Angular common directives
import { AuthService } from '../../Auth/auth..service'; // Import AuthService for authentication checks

@Component({ // Define the ItemDetailsComponent as an Angular component
  selector: 'app-item-details', // Component selector
  standalone: true, // Standalone component
  imports: [CommonModule], // Import required modules
  templateUrl: './item-details.component.html', // Template file
  styleUrl: './item-details.component.css' // Styles file
})
export class ItemDetailsComponent { // Define the ItemDetailsComponent class

item: Item | null = null; // Holds the item details, null if not loaded
  loading = false; // Flag to indicate loading state

  constructor(
    private route: ActivatedRoute, // Inject ActivatedRoute to access route parameters
    private itemService: ItemService, // Inject ItemService for item operations
    private authService: AuthService, // Inject AuthService for authentication
    private router: Router // Inject Router for navigation
  ) {}

  async ngOnInit(): Promise<void> { // Lifecycle hook: called on component initialization
    const id = Number(this.route.snapshot.paramMap.get('id')); // Get item ID from route parameters
    if (!id) { // If ID is invalid
      Swal.fire('Error', 'Invalid item ID.', 'error'); // Show error alert
      return; // Exit the function
    }

    this.loading = true; // Set loading to true while fetching data

    try {
      const res = await this.itemService.getItemById(id); // Fetch item details from ItemService
      if (res.status && res.data) { // If response is successful and data exists
        this.item = res.data; // Assign item data
      } else {
        Swal.fire('Error', res.message || 'Item not found.', 'error'); // Show error alert if item not found
      }
    } catch (err: any) { // Handle errors during fetch
      Swal.fire('Error', err.message || 'Failed to load item.', 'error'); // Show error alert
    } finally {
      this.loading = false; // Set loading to false after operation
    }
  }

addToCart() { // Method to add item to cart
  if (!this.authService.isLoggedIn()) { // If user is not logged in
    Swal.fire('Login Required', 'Please login to add items to cart.', 'warning').then(() => {
      this.router.navigate(['/login']); // Redirect to login page
    });
    return; // Exit the function
  }

  // If logged in
  Swal.fire('Added', 'Item added to cart.', 'success'); // Show success alert
}

buyNow() { // Method to buy the item immediately
  if (!this.authService.isLoggedIn()) { // If user is not logged in
    Swal.fire('Login Required', 'Please login to place an order.', 'warning').then(() => {
      this.router.navigate(['/login']); // Redirect to login page
    });
    return; // Exit the function
  }

  // If logged in
  Swal.fire('Ordered', 'Thank you for your purchase!', 'success'); // Show success alert
}



}
