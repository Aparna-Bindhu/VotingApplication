import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AppService } from '../services/app.service';
import { VotersModel } from './voters.model';

@Component({
  selector: 'app-voters',
  templateUrl: './voters.component.html',
  styleUrls: ['../app.component.css']
})
export class VotersComponent implements OnInit {
  @ViewChild('closeButton') closeButton: ElementRef;
  private VOTERS_API_URL: string = 'https://localhost:44328/api/voters/get';
  private ADD_NEW_VOTERS_API_URL: string = 'https://localhost:44328/api/voters/addvoter';
  public votersModel = new Array<VotersModel>();
  public newVoterName: string = '';
  public voterModel = new VotersModel();
  public erroMessage: string;
  public showError: boolean = false;

  constructor(public _service: AppService) { }

    ngOnInit(): void {
      this.getVotersList();
    }

  public getVotersList() {
    this._service.get(this.VOTERS_API_URL).subscribe(
      response =>
      {
        this.votersModel = response;
        console.log(this.votersModel);
        console.log(response);
      });
  }

  public AddNewVoter() {
    console.log('New Voter Name:', this.newVoterName);
    this.showError = false;
    this.erroMessage = '';
    if (this.newVoterName == null || this.newVoterName == undefined || this.newVoterName.trim() === '') {
      this.erroMessage = "Please enter a valid name.";
      this.showError = true;
      return;
    }
    else {
      if (this.newVoterName) {
        this.voterModel.Id = 0;
        this.voterModel.Name = this.newVoterName;
        this.voterModel.HasVoted = false;

        this.closeButton.nativeElement.click();

        this._service.save(this.ADD_NEW_VOTERS_API_URL, this.voterModel).subscribe(
          response => {
            if (response && response.length > 0) {
              console.log(response);
              this.votersModel = response;
            }
          });

      }
    }
  }
}

