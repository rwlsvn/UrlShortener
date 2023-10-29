import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styles: [
  ]
})
export class SignInComponent {
  type = 'password';
  eyeIcon = 'fa-eye-slash';
  loginErrorMessage = '';

  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, 
    private auth: AuthService, 
    private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onLogin() {
    if (this.loginForm.valid) {
      this.auth.login(this.loginForm.value)
        .subscribe({
          next: (res) => {
            console.log('Server Response:', res);
            console.log(res.accessToken);
            console.log(res.refreshToken);
            this.auth.setToken(res.accessToken);
            this.auth.setRefreshToken(res.refreshToken);
            this.router.navigate(['url-table']);
          },
          error: (err) => {
            console.error('Error:', err);
            this.loginErrorMessage = err.message;
          }
        });
    } else {
      this.validateFormFields(this.loginForm);
    }
  }

  hideShowPassword() {
    this.type = this.type == 'text' ? 'password' : 'text';
    this.eyeIcon = this.eyeIcon == 'fa-eye' ? 'fa-eye-slash' : 'fa-eye';
  }

  private validateFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsDirty({onlySelf: true});
      } else if (control instanceof FormGroup) {
        this.validateFormFields(control);
      }
    });
  }
}
