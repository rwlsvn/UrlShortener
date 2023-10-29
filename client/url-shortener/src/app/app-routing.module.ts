import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { UrlsTableComponent } from './pages/urls-table/urls-table.component';
import { AboutComponent } from './pages/about/about.component';

const routes: Routes = [
  {path: 'sign-in', component: SignInComponent},
  {path: 'sign-up', component: SignUpComponent},
  {path: 'url-table', component: UrlsTableComponent},
  {path: 'about', component: AboutComponent},
  {path: '', component: UrlsTableComponent},
  { path: '**', component: UrlsTableComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
