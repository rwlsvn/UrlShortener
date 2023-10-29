import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UrlApiService } from 'src/app/services/url-api.service';
import { UserApiService } from 'src/app/services/user-api.service';
import { UpdateAboutDialogComponent } from './components/update-about-dialog/update-about-dialog.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styles: [
  ]
})
export class AboutComponent {
  isEditEnable = false;
  aboutText = '';

  constructor(
    private urlApi: UrlApiService,
    private auth: AuthService,
    private addDialogRef: MatDialog,
  ){ }

  ngOnInit(){
    this.urlApi.getAboutText().subscribe(text => {
      this.aboutText = text;
    });
    this.isEditEnable = this.auth.getRolesFromToken().includes('admin');  
  }

  openUpdateAboutDialog(){
    this.addDialogRef.open(UpdateAboutDialogComponent, {
      data: this.aboutText
    });
  }
}
