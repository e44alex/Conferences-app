import { Session } from './../shared/model';
import { DataService } from './../shared/data.service';
import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';


@Component({
  selector: 'app-sessions',
  templateUrl: './sessions.component.html',
})
export class SessionsComponent implements OnInit {
  sessions: Session[];



  constructor(private dataService: DataService) { }

  getSessions(): void {
    this.dataService.getSessions()
    .then(sessions => this.sessions = sessions);
  }

  ngOnInit(): void {
    this.getSessions();
  }

}
