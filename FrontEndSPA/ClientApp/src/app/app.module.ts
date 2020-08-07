import { DataService } from './shared/data.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { SessionsComponent } from './sessions/sessions.component';
import { SessionDetailComponent } from './session-detail/session-detail.component';
import { SpeakerDetailComponent } from './speaker-detail/speaker-detail.component';
import { SpeakersComponent } from './speakers/speakers.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SessionsComponent,
    SessionDetailComponent,
    SpeakerDetailComponent,
    SpeakersComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'sessions', component: SessionsComponent},
      { path: 'speakers', component: SpeakersComponent},
      { path: 'sessiondetail/:id', component: SessionDetailComponent, pathMatch: 'full' },
      { path: 'speaker/:id', component: SpeakerDetailComponent, pathMatch: 'full' }
    ])
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
