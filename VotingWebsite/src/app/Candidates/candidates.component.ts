import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AppService } from '../services/app.service';
import { VotersModel } from '../Voters/voters.model';
import { CandidatesModel } from './candidates.model';

@Component({
  selector: 'app-candidates',
  templateUrl: './candidates.component.html',
  styleUrls: ['../app.component.css']
})
export class CandidatesComponent implements OnInit {
  @ViewChild('closeButton') closeButton: ElementRef;
  private CANDIDATES_API_URL: string = 'https://localhost:44328/api/candidates/get';
  private ADD_NEW_CANDIDATES_API_URL: string = 'https://localhost:44328/api/candidates/addcandidate';
  public votersModel = new Array<VotersModel>();
  public candidatesList = new Array<CandidatesModel>();
  public newCandidateName: string = '';
  public candidatesModel = new CandidatesModel();
  public erroMessage: string;
  public showError: boolean = false;

  constructor(public _service: AppService) { }

  ngOnInit(): void {
    this.getCandidateList();
  }

  public getCandidateList() {
    this._service.get(this.CANDIDATES_API_URL).subscribe(
      response => {
        this.candidatesList = response;
        console.log(this.candidatesList);
        console.log(response);
      });
  }

  public AddNewCandidate() {
    console.log('New Candidates Name:', this.newCandidateName);
    this.showError = false;
    this.erroMessage = '';
    if (this.newCandidateName == null || this.newCandidateName == undefined || this.newCandidateName.trim() === '') {
      this.erroMessage = "Please enter a valid name.";
      this.showError = true;
      return;
    }
    else {
      if (this.newCandidateName) {
        this.candidatesModel.Id = 0;
        this.candidatesModel.Name = this.newCandidateName;
        this.candidatesModel.Votes = 0;

        this.closeButton.nativeElement.click();

        this._service.save(this.ADD_NEW_CANDIDATES_API_URL, this.candidatesModel).subscribe(
          response => {
            if (response && response.length > 0) {
              console.log(response);
              this.candidatesList = response;
            }
          });

      }
    }
  }

  public VoteCasting(selectedVoterId: number, selectedCandidateId: number): void {
    console.log('Selected Voter Id:', selectedVoterId);
    console.log('Selected Candidate Id:', selectedCandidateId);
    // Perform further actions with the selected voter and candidate Ids
  }
}
