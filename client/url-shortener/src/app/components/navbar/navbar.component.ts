import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styles: [
  ]
})
export class NavbarComponent {
  username = '';
  isLoggedIn!: boolean;

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }
  
  ngOnInit(){
    if (this.auth.isLoggedIn()){
      this.username = this.auth.getUsernameFromToken();
      this.isLoggedIn = true;
    }   
  }
  
  onSignOut(){
    this.auth.signOut();
    this.router.navigate(['sign-in'])
  }
}
