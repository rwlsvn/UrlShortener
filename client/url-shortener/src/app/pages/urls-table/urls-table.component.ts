import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ShortenedUrlModel } from 'src/app/models/urls/shortened-url.model';
import { UrlApiService } from 'src/app/services/url-api.service';
import { AddUrlDialogComponent } from './components/add-url-dialog/add-url-dialog.component';
import { DeleteUrlDialogComponent } from './components/delete-url-dialog/delete-url-dialog.component';
import { UrlInfoDialogComponent } from './components/url-info-dialog/url-info-dialog.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-urls-table',
  templateUrl: './urls-table.component.html',
  styles: [
  ]
})
export class UrlsTableComponent {
  shortUrlBase = 'https://localhost:7061/su/'
  urls: ShortenedUrlModel[] = [];

  roles: string[] = [];
  userId = '';
  isLoggedIn = false;

  constructor(
    private urlApi: UrlApiService,
    private auth: AuthService,
    private addDialogRef: MatDialog
  ) { }

  ngOnInit(){
    this.urlApi.getAllUrls().subscribe(res => {
      this.urls = res;
      console.log(this.urls);
    });

    if (this.auth.isLoggedIn()){
      this.roles = this.auth.getRolesFromToken();
      this.userId = this.auth.getIdFromToken();
      this.isLoggedIn = true;
    }   
  }

  openAddUrlDialog(){
    this.addDialogRef.open(AddUrlDialogComponent);
  }

  openUrlInfoDialog(id: string){
    this.addDialogRef.open(UrlInfoDialogComponent, {
      data: id
    });
  }

  openDeleteUrlDialog(id: string){
    const dialogRef = this.addDialogRef.open(DeleteUrlDialogComponent, {
      data: id
    });

    dialogRef.afterClosed().subscribe((response: boolean) => {
      if(response){
        this.urls = this.urls.filter(url => url.id !== id);
      }
    });
  }
}
