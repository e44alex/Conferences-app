import { DataService } from './../shared/data.service';
import { Speaker } from './../shared/model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Params } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-speaker-detail',
  templateUrl: './speaker-detail.component.html',
  styleUrls: ['./speaker-detail.component.css']
})
export class SpeakerDetailComponent implements OnInit {
  speaker: Speaker;

  constructor(
    private dataService: DataService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit() {
    this.route.params
    .forEach((param: Params) => {
      if (param['id'] !== undefined) {
        const id = +param['id'];
        this.dataService.getSpeaker(id)
          .then(speaker => this.speaker = speaker);
      }
    });
  }

  goBack() {
    this.location.back();
  }
}
