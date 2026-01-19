import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { RegisterDto } from '../../models/auth.model';
import { Auth } from '../../services/auth';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  registerObj: RegisterDto = {
    email: '',
    password: '',
    confirmPassword: '',
  };

  constructor(private authService: Auth, private router: Router) {}

  onRegister() {
    this.authService.register(this.registerObj).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          Swal.fire('Success', 'Account created! Please login.', 'success');
          this.router.navigate(['/login']);
        }
      },
      error: (err) => {
        Swal.fire('Error', err.error.message || 'Registration failed', 'error');
      },
    });
  }
}
