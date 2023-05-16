import { DataService } from './../shared/data.service';
import { Speaker } from './../shared/model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-speakers',
  templateUrl: './speakers.component.html',
  styleUrls: ['./speakers.component.css']
})
export class SpeakersComponent implements OnInit {
  speakers: Speaker[];

  constructor(private dataService: DataService) { }

getSpeakers(): void {
  this.dataService.getSpeakers()
    .then(speakers => this.speakers = speakers);
}

  ngOnInit(): void {
    this.getSpeakers();
  }

}
