import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  loginObj = { email: '', password: '' };

  constructor(
    private authService: Auth,
    private router: Router,
  ) {}
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.router.navigate(['/dashboard']);
    }
  }

  onLogin() {
    this.authService.login(this.loginObj).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          Swal.fire('Success!', 'Welcome Lets Goo!', 'success');
          this.router.navigate(['/dashboard'], { replaceUrl: true });
        }
      },
      error: (err) => {
        Swal.fire('Error', 'Invalid login credentials', 'error');
      },
    });
  }
}
