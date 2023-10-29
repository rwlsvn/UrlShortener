import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterResponse } from 'src/app/models/auth/register-response';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styles: [
  ]
})
export class SignUpComponent {
  type = 'password';
  eyeIcon = 'fa-eye-slash';

  signUpErrorMessage = '';

  signUpForm!: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  hideShowPassword() {
    this.type = this.type == 'text' ? 'password' : 'text';
    this.eyeIcon = this.eyeIcon == 'fa-eye' ? 'fa-eye-slash' : 'fa-eye';
  }

  onSignup() {
    if (this.signUpForm.valid) {  
      console.log(this.signUpForm.value);
      this.auth.register(this.signUpForm.value).subscribe({
        next: (response: RegisterResponse) => {
          if (response.succeeded === false){
            console.log(response.errors[0].description);
            this.signUpErrorMessage = response.errors[0].description;
          } else {
            this.router.navigate(['sign-in']);
          }
        },
        error: (err) => {
          console.log(err.error.message);
        }
      });
    } else {
      this.validateFormFields(this.signUpForm);
    }
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
