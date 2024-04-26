import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CostumerComponent } from './components/costumer/costumer.component';
import { HttpClientModule} from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { FrontpageComponent } from './components/frontpage/frontpage.component';
import { AreapickComponent } from './components/areapick/areapick.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MovieComponent } from './components/movie/movie.component';
import { AdminFrontPageComponent } from './components/admin/admin-front-page/adminFrontPage.component';
import { AdminMovieComponent } from './components/admin/admin-movie/adminmovie.component';

@NgModule({
  declarations: [
    AppComponent,
    CostumerComponent,
    LoginComponent,
    FrontpageComponent,
    AreapickComponent,
    MovieComponent,
    AdminFrontPageComponent,
    AdminMovieComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
