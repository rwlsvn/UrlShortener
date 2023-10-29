import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AuthService } from 'src/app/services/auth.service';
import { UrlApiService } from 'src/app/services/url-api.service';

@Component({
  selector: 'app-delete-url-dialof]g',
  templateUrl: './delete-url-dialog.component.html',
  styles: [
  ]
})
export class DeleteUrlDialogComponent {
  constructor(
    private urlApi: UrlApiService,
    private auth: AuthService,
    private dialogRef: MatDialogRef<DeleteUrlDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public deletedUrlId: string
  ){ }

  onUrlDelete() {
    if (this.auth.getRolesFromToken().includes('admin')){
      this.urlApi.deleteAdminUrl(this.deletedUrlId).subscribe(
        () => {
          this.dialogRef.close(true);
        },
        (error) => {
          console.error('Error adding product:', error);
        }
      );
    } else {
      this.urlApi.deleteUserUrl(this.deletedUrlId).subscribe(
        () => {
          this.dialogRef.close(true);
        },
        (error) => {
          console.error('Error adding product:', error);
        }
      );
    }  
  }

  onCloseDialog(): void {
    this.dialogRef.close(false);
  }
}
