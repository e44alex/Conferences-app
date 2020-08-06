import { environment } from './../../environments/environment.prod';
import { Injectable, Inject } from '@angular/core';


import { Session, Speaker } from './model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class DataService {

  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });


  private sessionUrl = 'api/sessions';
  private speakerUrl = 'api/speakers';
  /**
   * init with Http
   */
  constructor(private http: HttpClient, @Inject('API_URL') private baseUrl: string) { }



  getSessions(): Promise<Session[]> {
    return new Promise((resolve, reject) => {
        const path = this.baseUrl + this.sessionUrl;
        console.log(path);
        this.http.get('https://localhost:44363/api/Sessions')
        .subscribe( (data: Session[]) => {
          console.log(data);
          return resolve(data);
        });
    });
  }

  getSession(id: number): Promise<Session> {
    const url = `${this.baseUrl + this.sessionUrl}/${id}`;
    return new Promise((resolve, reject) => {
      const path = this.baseUrl + this.speakerUrl;
      this.http.get(path)
      .subscribe( (data: Session) => {
        return resolve(data);
      });
    });
  }

  getSpeaker(id: number): Promise<Speaker> {


    const url = `${this.baseUrl + this.speakerUrl}/${id}`;

    return new Promise((resolve, reject) => {
      const path = this.baseUrl + this.speakerUrl;
      this.http.get(path)
      .subscribe( (data: Speaker) => {
        return resolve(data);
      });
    });
  }

  getSpeakers(): Promise<Speaker[]> {
    return new Promise((resolve, reject) => {
      const path = this.baseUrl + this.speakerUrl;
      this.http.get(path)
      .subscribe( (data: Speaker[]) => {
        return resolve(data);
      });
    });
  }

  private getData(response: Response) { }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}

