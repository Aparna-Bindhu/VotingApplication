import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public title: string;
  public isCastVotePageVisible: boolean = false;
  ngOnInit(): void {
    this.title = 'Voting App';
  }
  public setVisibityForCastVotePage() {
    this.isCastVotePageVisible = true;
  }

  public hideVisibityForCastVotePage() {
    this.isCastVotePageVisible = false;
  }
}
