import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { FrontpageComponent } from './components/frontpage/frontpage.component';
import { AreapickComponent } from './components/areapick/areapick.component';
import { AdminFrontPageComponent } from './components/admin/admin-front-page/adminFrontPage.component';
import { AdminMovieComponent } from './components/admin/admin-movie/adminmovie.component';
import { authGuard, adminGuard } from './guard/auth.guard';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { AdmingenreComponent } from './components/admin/admin-genre/admingenre.component';
import { AdminCinemaComponent } from './components/admin/admin-cinema/admincinema.component';
import { AdmincinemaHallComponent } from './components/admin/admincinema-hall/admincinemahall.component';
import { AdminregionComponent } from './components/admin/adminregion/adminregion.component';
import { AdminareaComponent } from './components/admin/adminarea/adminarea.component';

const routes: Routes = [
  { path: '', component: FrontpageComponent},
  { path: 'profile', component: FrontpageComponent, canActivate : [authGuard]},
  { path: 'admin', component: AdminFrontPageComponent, canActivate : [adminGuard]},
  { path: 'login', component: LoginComponent },
  { path: 'areapick', component: AreapickComponent },
  { path: 'adminmovie', component: AdminMovieComponent},
  { path: 'admingenre', component: AdmingenreComponent},
  { path: 'admincinema', component: AdminCinemaComponent},
  { path: 'admincinemahall', component: AdmincinemaHallComponent},
  { path: 'adminregion', component: AdminregionComponent},
  { path: 'adminarea', component: AdminareaComponent},
  { path: 'unauthorized', component: UnauthorizedComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
