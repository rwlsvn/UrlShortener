import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { UrlsTableComponent } from './pages/urls-table/urls-table.component';
import { AddUrlDialogComponent } from './pages/urls-table/components/add-url-dialog/add-url-dialog.component';
import { DeleteUrlDialogComponent } from './pages/urls-table/components/delete-url-dialog/delete-url-dialog.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UrlInfoDialogComponent } from './pages/urls-table/components/url-info-dialog/url-info-dialog.component';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { AboutComponent } from './pages/about/about.component';
import { UpdateAboutDialogComponent } from './pages/about/components/update-about-dialog/update-about-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    SignUpComponent,
    UrlsTableComponent,
    AddUrlDialogComponent,
    DeleteUrlDialogComponent,
    NavbarComponent,
    UrlInfoDialogComponent,
    AboutComponent,
    UpdateAboutDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatDialogModule,
    FormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
