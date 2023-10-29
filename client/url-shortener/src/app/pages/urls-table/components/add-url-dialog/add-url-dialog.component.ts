import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AddUrlResponse } from 'src/app/models/urls/add-url-response';
import { UrlApiService } from 'src/app/services/url-api.service';

@Component({
  selector: 'app-add-url-dialog',
  templateUrl: './add-url-dialog.component.html',
  styles: [
  ]
})
export class AddUrlDialogComponent {
  addUrlForm!: FormGroup
  addUrlErrorMessage = '';

  constructor(
    private fb: FormBuilder,
    private urlApi: UrlApiService,
    public dialogRef: MatDialogRef<AddUrlDialogComponent>
  ) { }

  ngOnInit() {
    this.addUrlForm = this.fb.group({
      originalUrl: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  onAddUrl() {
    if (this.addUrlForm.valid){
      this.urlApi.addUrl(this.addUrlForm.value).subscribe({
        next: (response: AddUrlResponse) => {
          if (response.succeeded === false){
            console.log(response.errors[0].description);
            this.addUrlErrorMessage = response.errors[0].description;
          } else {
            this.dialogRef.close();
          }
        },
        error: (err) => {
          console.log(err.error.message);
        }
      })
    }
  }

  onCloseDialog(): void {
    this.dialogRef.close();
  }
}
