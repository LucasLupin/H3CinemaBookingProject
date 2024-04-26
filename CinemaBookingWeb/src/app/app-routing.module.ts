import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { FrontpageComponent } from './components/frontpage/frontpage.component';
import { AreapickComponent } from './components/areapick/areapick.component';
import { AdminFrontPageComponent } from './components/admin/admin-front-page/adminFrontPage.component';
import { AdminMovieComponent } from './components/admin/admin-movie/adminmovie.component';

const routes: Routes = [
  { path: '', component: FrontpageComponent},
  { path: 'admin', component: AdminFrontPageComponent},
  { path: 'login', component: LoginComponent },
  { path: 'areapick', component: AreapickComponent },
  { path: 'adminmovie', component: AdminMovieComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
