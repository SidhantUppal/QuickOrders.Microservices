import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { LoginRequest } from '../../Core/Services/Models/user.model';
import { AuthService } from '../auth..service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

 loginData: LoginRequest = {
    email: '',
    password: ''
  };

  constructor(private authService: AuthService,private router: Router) {}

  async onLogin() {
  try {
    const res = await this.authService.login(this.loginData);
    console.log('Login response:', res);
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
    Swal.fire('Error', 'Something went wrong during login.', 'error');
  }
}

navigateToRegister(): void {
  this.router.navigate(['/register']);
}

}
