import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UrlApiService } from 'src/app/services/url-api.service';

@Component({
  selector: 'app-update-about-dialog',
  templateUrl: './update-about-dialog.component.html',
  styles: [
  ]
})
export class UpdateAboutDialogComponent {
  updatedAboutText = '';
  updateAboutErrorMessage = '';

  constructor(
    private urlApi: UrlApiService,
    private dialogRef: MatDialogRef<UpdateAboutDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public aboutText: string
  ) { }

  ngOnInit(){
    this.updatedAboutText = this.aboutText;
  }

  onSaveAboutText() {
    this.urlApi.updateAboutText(this.updatedAboutText).subscribe({
      next: () => {
        this.dialogRef.close();
      },
      error: (err) => {
        this.updateAboutErrorMessage = 'Something went wrong';
        console.log(err.error.message);
      }
    })   
  }

  onCloseDialog(): void {
    this.dialogRef.close();
  }
}
