import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LoginRequest, RegisterRequest } from '../../Core/Services/Models/user.model';
import { AuthService } from '../auth..service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

registerData: RegisterRequest = {
    email: '',
    password: '',
    username: '' // If you added this to LoginRequest model
  };

  constructor(private authService: AuthService, private router: Router) {}

  async onRegister() {
    try {
      const res = await this.authService.register(this.registerData);
      if (res.status && res.data) {
        Swal.fire('Success!', res.message, 'success');
        // Store user info in sessionStorage
        sessionStorage.setItem('user', JSON.stringify(res.data));
        // Redirect to home
        this.router.navigate(['/']);
      } else {
        Swal.fire('Failed', res.message, 'error');
      }
    } catch (error) {
      Swal.fire('Error', 'Something went wrong during registration.', 'error');
    }
  }

  navigateToLogin(): void {
    this.router.navigate(['/login']);
  }

}
