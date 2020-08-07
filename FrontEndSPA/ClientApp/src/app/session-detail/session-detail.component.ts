import { DataService } from './../shared/data.service';
import { Session } from './../shared/model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Params } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Location } from '@angular/common';

@Component({
  selector: 'app-session-detail',
  templateUrl: './session-detail.component.html',
  styleUrls: ['./session-detail.component.css']
})
export class SessionDetailComponent implements OnInit {

  session: Session;

  constructor(
    private sessionService: DataService,
    private route: ActivatedRoute,
    private  location: Location
    ) { }

  ngOnInit(): void {

    this.route.params
      .forEach((param: Params) => {
        if (param['id'] !== undefined) {
          const id = +param['id'];
          console.log(id);
          this.sessionService.getSession(id)
            .then(session => this.session = session);
          console.log(this.session);
        }
      });
  }

  goBack() {
    this.location.back();
  }

}
