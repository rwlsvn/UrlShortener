import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ShortenedUrlDetailsModel } from 'src/app/models/urls/shortened-url-details.model';
import { UrlApiService } from 'src/app/services/url-api.service';
import { UserApiService } from 'src/app/services/user-api.service';

@Component({
  selector: 'app-url-info-dialog',
  templateUrl: './url-info-dialog.component.html',
  styles: [
  ]
})
export class UrlInfoDialogComponent {
  url: ShortenedUrlDetailsModel = {
    id: '',
    userId: '',
    originalUrl: '',
    url: '',
    description: '',
    creationDate: new Date()
  };
  username = '';

  shortUrlBase = 'https://localhost:7061/su/'

  constructor(
    private urlApi: UrlApiService,
    private userApi: UserApiService,
    private dialogRef: MatDialogRef<UrlInfoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public urlId: string
  ){ }

  ngOnInit(){
    this.urlApi.getDetailedUrl(this.urlId).subscribe(url => {
      this.url = url;
      this.userApi.getUserName(url.userId).subscribe(res =>{
        this.username = res.name;
      });
    }); 
  }

  onCloseDialog(): void {
    this.dialogRef.close();
  }
}
