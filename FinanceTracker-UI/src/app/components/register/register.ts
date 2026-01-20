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

  constructor(
    private authService: Auth,
    private router: Router,
  ) {}

  onRegister() {
    this.authService.register(this.registerObj).subscribe({
      next: (res) => {
        Swal.fire({
          title: 'Success!',
          text: 'Account created successfully. Welcome aboard!',
          icon: 'success',
          confirmButtonColor: '#198754',
        }).then(() => {
          this.router.navigateByUrl('/login');
        });
      },
      error: (err) => {
        console.log('Backend Error handled properly');

        let errorMessage = 'Something went wrong, please try again later';

        if (err.status === 400) {
          errorMessage = err.error?.message || 'Email already exists or data is invalid';
        }

        Swal.fire({
          title: 'Registration Failed',
          text: errorMessage,
          icon: 'error',
          confirmButtonColor: '#dc3545',
        });
      },
    });
  }
}
