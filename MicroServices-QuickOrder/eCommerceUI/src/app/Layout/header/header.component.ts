// import { CommonModule } from '@angular/common';
// import { Component } from '@angular/core';
// import { RouterLink } from '@angular/router';

// @Component({
//   selector: 'app-header',
//   standalone: true,
//   imports: [CommonModule, RouterLink],
//   templateUrl: './header.component.html',
//   styleUrl: './header.component.css'
// })
// export class HeaderComponent {

//  username: string | null = null;

//   ngOnInit() {
//     const user = sessionStorage.getItem('user');
//     if (user) {
//       const parsed = JSON.parse(user);
//       this.username = parsed.name;
//     }
//   }

// }


import { CommonModule } from '@angular/common'; // Import CommonModule for Angular common directives
import { Component, OnDestroy, OnInit } from '@angular/core'; // Import Component, OnInit, and OnDestroy interfaces
import { Router, RouterLink, NavigationEnd } from '@angular/router'; // Import Router, RouterLink, and NavigationEnd for navigation and event handling
import { Subscription } from 'rxjs'; // Import Subscription for managing observable subscriptions

@Component({ // Define the HeaderComponent as an Angular component
  selector: 'app-header', // Component selector
  standalone: true, // Standalone component
  imports: [CommonModule, RouterLink], // Import required modules
  templateUrl: './header.component.html', // Template file
  styleUrl: './header.component.css' // Styles file
})
export class HeaderComponent implements OnInit, OnDestroy { // Define the HeaderComponent class implementing OnInit and OnDestroy
  username: string | null = null; // Holds the username of the logged-in user, null if not logged in
  private routerEventsSub!: Subscription; // Subscription to router events

  constructor(private router: Router) {} // Inject Router for navigation and event handling

  ngOnInit() { // Lifecycle hook: called on component initialization
    this.updateUsername(); // Set username on initialization

    // Watch for route changes to re-check login state
    this.routerEventsSub = this.router.events.subscribe(event => { // Subscribe to router events
      if (event instanceof NavigationEnd) { // If navigation ends
        this.updateUsername(); // Update username (in case login state changed)
      }
    });
  }

  ngOnDestroy() { // Lifecycle hook: called when component is destroyed
    if (this.routerEventsSub) { // If subscription exists
      this.routerEventsSub.unsubscribe(); // Unsubscribe to prevent memory leaks
    }
  }

  updateUsername() { // Method to update the username from session storage
    const user = sessionStorage.getItem('user'); // Get user from session storage
    if (user) { // If user exists
      const parsed = JSON.parse(user); // Parse user object
      this.username = parsed.username; // Set username (ensure property is 'username')
    } else {
      this.username = null; // Set to null if not logged in
    }
  }

  logout() { // Method to log out the user
    sessionStorage.removeItem('user'); // Remove user from session storage
    this.username = null; // Clear username
    this.router.navigate(['/']); // Redirect to home page
  }
}
