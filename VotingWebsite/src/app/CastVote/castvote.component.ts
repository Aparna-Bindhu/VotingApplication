import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AppService } from '../services/app.service';
import { VotersModel } from '../Voters/voters.model';
import { CandidatesModel } from '../Candidates/candidates.model';

@Component({
  selector: 'app-castvote',
  templateUrl: './castvote.component.html',
  styleUrls: ['../app.component.css']
})
export class CastvoteComponent implements OnInit {
  @ViewChild('closeButton') closeButton: ElementRef;
  private CANDIDATES_API_URL: string = 'https://localhost:44328/api/Candidates/get';
  private VOTERS_API_URL: string = 'https://localhost:44328/api/Voters/unvotedvoters';
  private CANDIDATES_VOTERS_UPDATE_API_URL: string = 'https://localhost:44328/api/candidates/update';
  public votersList = new Array<VotersModel>();
  public candidatesList = new Array<CandidatesModel>();
  public errorMessage: string;
  public successMessage: string;
  public showError: boolean = false;
  public showSuccess: boolean = false;

  constructor(public _service: AppService) { }

  ngOnInit(): void {
    this.getCandidateList();
    this.getUnVotedVotersList();
  }

  public getCandidateList() {
    this._service.get(this.CANDIDATES_API_URL).subscribe(
      response => {
        if (response) {
          this.candidatesList = response;
          console.log(this.candidatesList);
          console.log(response);
        }
      });
  }

  public getUnVotedVotersList() {
    this._service.get(this.VOTERS_API_URL).subscribe(
      response => {
        if (response) {
          this.votersList = response;
          console.log(response);
        }
      });
  }

  public VoteCasting(selectedVoterId: number, selectedCandidateId: number): void {
    console.log('Selected Voter Id:', selectedVoterId);
    console.log('Selected Candidate Id:', selectedCandidateId);
    this.showError = false;
    this.showSuccess = false;
    this.errorMessage = "";
    this.successMessage = ""
    if (selectedVoterId > 0 && selectedCandidateId > 0) {
      var data = {
        VotersId: selectedVoterId,
        CandidatesId: selectedCandidateId
      };

      this._service.save(this.CANDIDATES_VOTERS_UPDATE_API_URL, data).subscribe(
        response => {
          if (response) {
            console.log(response);
            this.getCandidateList();
            this.getUnVotedVotersList();
            this.successMessage = "Vote casted successfully."
            this.showSuccess = true;
            this.showError = false;
            this.errorMessage = "";
          }
        });
    }
    else {
      this.showError = true;
      this.errorMessage = "Please select both.";

      this.successMessage = ""
      this.showSuccess = false;
    }
  }
}
