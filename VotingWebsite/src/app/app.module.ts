import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { VotersComponent } from './Voters/voters.component';
import { CandidatesComponent } from './Candidates/candidates.component';
import { CastvoteComponent } from './CastVote/castvote.component';
import { AppService } from './services/app.service'


@NgModule({
  declarations: [
    AppComponent,
    VotersComponent,
    CandidatesComponent,
    CastvoteComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [AppService],
  bootstrap: [AppComponent]
})
export class AppModule { }
